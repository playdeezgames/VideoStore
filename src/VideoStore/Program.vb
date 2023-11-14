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
                        prompt.AddChoice(fullname)
                        table(fullname) = item.Id
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
        Dim name = AnsiConsole.Ask("New Category Name? ", String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask("New Category Abbreviation?", String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = "SELECT COUNT(1) FROM Categories WHERE CategoryAbbr=@CategoryAbbr;"
        command.Parameters.AddWithValue("@CategoryAbbr", abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine("[red]Duplicate Abbreviation![/]")
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = "INSERT INTO Categories(CategoryName, CategoryAbbr) VALUES(@CategoryName, @CategoryAbbr);"
        command.Parameters.AddWithValue("@CategoryName", name)
        command.Parameters.AddWithValue("@CategoryAbbr", abbreviation)
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
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
