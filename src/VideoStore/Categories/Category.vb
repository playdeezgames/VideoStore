Imports Microsoft.Data.SqlClient

Friend Module Category
    Friend Sub Run(store As DataStore, categoryId As Integer)
        Do
            Dim category As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer) = store.Category(categoryId)
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {category.Id}")
            AnsiConsole.MarkupLine($"Abbreviation: {category.Abbr}")
            AnsiConsole.MarkupLine($"Name: {category.Name}")
            AnsiConsole.MarkupLine($"Media: {category.MediaCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = NowWhat}
            prompt.AddChoice(GoBack)
            prompt.AddChoice(ChangeName)
            prompt.AddChoice(ChangeAbbreviation)
            If category.MediaCount = 0 Then
                prompt.AddChoice(MenuItems.DeleteCategory)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBack
                    Exit Do
                Case MenuItems.DeleteCategory
                    DeleteCategory.Run(store, categoryId)
                    Exit Do
                Case ChangeName
                    ChangeCategoryName(store, categoryId, category.Name)
                Case ChangeAbbreviation
                    ChangeCategoryAbbreviation(store, categoryId, category.Abbr)
            End Select
        Loop
    End Sub


    Private Sub ChangeCategoryAbbreviation(store As DataStore, categoryId As Integer, abbr As String)
        Dim newAbbreviation = AnsiConsole.Ask("[olive]New Abbreviation?[/]", abbr)
        If newAbbreviation <> abbr Then
            Dim command = store.Connection.CreateCommand
            command.CommandText = CategoryUpdateAbbreviation
            command.Parameters.AddWithValue(Parameters.CategoryAbbr, newAbbreviation)
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
    Private Sub ChangeCategoryName(store As DataStore, categoryId As Integer, name As String)
        Dim newName = AnsiConsole.Ask("[olive]New Name?[/]", name)
        If newName <> name Then
            Dim command = store.Connection.CreateCommand
            command.CommandText = CategoryUpdateName
            command.Parameters.AddWithValue(Parameters.CategoryName, newName)
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
End Module
