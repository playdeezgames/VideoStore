Imports Microsoft.Data.SqlClient

Friend Module Category
    Friend Sub RunCategory(connection As SqlConnection, categoryId As Integer)
        Do
            Dim category As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer)
            Dim command = connection.CreateCommand()
            command.CommandText = CategoryDetails
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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = NowWhat}
            prompt.AddChoice(GoBackItemText)
            prompt.AddChoice(ChangeNameItemText)
            prompt.AddChoice(ChangeAbbreviationItemText)
            If category.MediaCount = 0 Then
                prompt.AddChoice(DeleteCategoryItemText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackItemText
                    Exit Do
                Case DeleteCategoryItemText
                    DeleteCategory.DeleteCategory(connection, categoryId)
                    Exit Do
                Case ChangeNameItemText
                    ChangeCategoryName(connection, categoryId, category.Name)
                Case ChangeAbbreviationItemText
                    ChangeCategoryAbbreviation(connection, categoryId, category.Abbr)
            End Select
        Loop
    End Sub


    Private Sub ChangeCategoryAbbreviation(connection As SqlConnection, categoryId As Integer, abbr As String)
        Dim newAbbreviation = AnsiConsole.Ask("[olive]New Abbreviation?[/]", abbr)
        If newAbbreviation <> abbr Then
            Dim command = connection.CreateCommand
            command.CommandText = CategoryUpdateAbbreviation
            command.Parameters.AddWithValue(CategoryAbbrParameterName, newAbbreviation)
            command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
    Private Sub ChangeCategoryName(connection As SqlConnection, categoryId As Integer, name As String)
        Dim newName = AnsiConsole.Ask("[olive]New Name?[/]", name)
        If newName <> name Then
            Dim command = connection.CreateCommand
            command.CommandText = CategoryUpdateName
            command.Parameters.AddWithValue(CategoryNameParameterName, newName)
            command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
End Module
