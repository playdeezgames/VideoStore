Imports System.ComponentModel.DataAnnotations
Imports Microsoft.Data.SqlClient

Module Program
    Private Const CategoriesText As String = "Categories"
    Private Const QuitText As String = "Quit"
    Private Const GoBackText As String = "Go Back"
    Private Const MainMenuHeader As String = "[olive]Main Menu:[/]"
    Private Const CategoriesMenuHeader As String = "[olive]Categories Menu:[/]"
    Private Const NewCategoryText As String = "New Category..."
    Private Const CategoryListCommandText As String = "SELECT CategoryId, CategoryAbbr, CategoryName FROM Categories ORDER BY CategoryName;"
    Private Const CategoryDetailCommandText As String = "
SELECT 
    c.CategoryAbbr, 
    c.CategoryName, 
    COUNT(m.MediaId) MediaCount
FROM 
    Categories c 
    LEFT JOIN Media m ON m.CategoryId=c.CategoryId
GROUP BY
    c.CategoryId,
    c.CategoryAbbr,
    c.CategoryName
HAVING 
    c.CategoryId=@CategoryId;"
    Private Const CategoryIdParameterName As String = "@CategoryId"
    Private Const NowWhatMenuHeader As String = "[olive]Now What?[/]"
    Private Const OkText As String = "Ok"
    Private Const NewCategoryNamePrompt As String = "[olive]New Category Name?[/]"
    Private Const NewCategoryAbbreviationPrompt As String = "[olive]New Category Abbreviation?[/]"
    Private Const CategoryCheckAbbreviationCommandText As String = "SELECT COUNT(1) FROM Categories WHERE CategoryAbbr=@CategoryAbbr;"
    Private Const CategoryAbbrParameterText As String = "@CategoryAbbr"
    Private Const DuplicateAbbreviationErrorText As String = "[red]Duplicate Abbreviation![/]"
    Private Const AddCategoryCommandText As String = "INSERT INTO Categories(CategoryName, CategoryAbbr) VALUES(@CategoryName, @CategoryAbbr);"
    Private Const CategoryNameParameterText As String = "@CategoryName"
    Private Const DeleteCategoryText As String = "Delete Category"

    Sub Main(args As String())
        Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=MediaLibrary;Integrated Security=true;TrustServerCertificate=true")
            connection.Open()
            DoMainMenu(connection)
            connection.Close()
        End Using
    End Sub

    Private Sub DoMainMenu(connection As SqlConnection)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MainMenuHeader}
            prompt.AddChoice(CategoriesText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case CategoriesText
                    RunCategories(connection)
                Case QuitText
                    Exit Do
            End Select
        Loop
    End Sub


    Private Sub RunCategories(connection As SqlConnection)
        Do
            Dim categoryList As New List(Of (Id As Integer, Abbr As String, Name As String))
            Dim prompt As New SelectionPrompt(Of String) With {.Title = CategoriesMenuHeader}
            prompt.AddChoice(GoBackText)
            prompt.AddChoice(NewCategoryText)
            Dim table As New Dictionary(Of String, Integer)
            Using command = connection.CreateCommand
                command.CommandText = CategoryListCommandText
                Using reader = command.ExecuteReader
                    While reader.Read
                        Dim item As (Id As Integer, Abbr As String, Name As String) = (reader.GetInt32(0), reader.GetString(1), reader.GetString(2))
                        categoryList.Add(item)
                        Dim fullName = $"{item.Name}({item.Abbr})#{item.Id}"
                        prompt.AddChoice(fullName)
                        table(fullName) = item.Id
                    End While
                End Using
            End Using
            AnsiConsole.Clear()
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case NewCategoryText
                    RunNewCategory(connection)
                Case Else
                    RunCategory(connection, table(answer))
            End Select
        Loop
    End Sub
    Private Sub RunNewCategory(connection As SqlConnection)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryNamePrompt, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviationPrompt, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviationCommandText
        command.Parameters.AddWithValue(CategoryAbbrParameterText, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(DuplicateAbbreviationErrorText)
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = AddCategoryCommandText
        command.Parameters.AddWithValue(CategoryNameParameterText, name)
        command.Parameters.AddWithValue(CategoryAbbrParameterText, abbreviation)
        command.ExecuteNonQuery()
    End Sub

    Private Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub

    Private Sub RunCategory(connection As SqlConnection, categoryId As Integer)
        Do
            Dim category As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer)
            Dim command = connection.CreateCommand()
            command.CommandText = CategoryDetailCommandText
            command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
            Using reader = command.ExecuteReader()
                reader.Read()
                category = (categoryId, reader.GetString(0), reader.GetString(1), reader.GetInt32(2))
            End Using
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {category.Id}")
            AnsiConsole.MarkupLine($"Abbreviation: {category.Abbr}")
            AnsiConsole.MarkupLine($"Name: {category.Name}")
            AnsiConsole.MarkupLine($"Media: {category.MediaCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = NowWhatMenuHeader}
            prompt.AddChoice(GoBackText)
            If category.MediaCount = 0 Then
                prompt.AddChoice(DeleteCategoryText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    Exit Do
                Case DeleteCategoryText
                    DeleteCategory(connection, categoryId)
                    Exit Do
            End Select
        Loop
    End Sub
    Private Sub DeleteCategory(connection As SqlConnection, categoryId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = "DELETE FROM Categories WHERE CategoryId=@CategoryId;"
        command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
        command.ExecuteNonQuery()
    End Sub
End Module
