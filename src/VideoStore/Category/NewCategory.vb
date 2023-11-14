Imports Microsoft.Data.SqlClient

Friend Module NewCategory
    Friend Sub RunNewCategory(connection As SqlConnection)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryNamePromptText, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviationPromptText, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviationCommandText
        command.Parameters.AddWithValue(CategoryAbbrParameterName, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(DuplicateAbbreviationErrorText)
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = CategoryInsertCommandText
        command.Parameters.AddWithValue(CategoryNameParameterName, name)
        command.Parameters.AddWithValue(CategoryAbbrParameterName, abbreviation)
        command.ExecuteNonQuery()
    End Sub
End Module
