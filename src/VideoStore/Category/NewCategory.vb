Imports Microsoft.Data.SqlClient

Friend Module NewCategory
    Friend Sub Run(connection As SqlConnection)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryName, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviation, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviation
        command.Parameters.AddWithValue(CategoryAbbr, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(DuplicateAbbreviation)
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = CategoryInsert
        command.Parameters.AddWithValue(CategoryName, name)
        command.Parameters.AddWithValue(CategoryAbbr, abbreviation)
        command.ExecuteNonQuery()
    End Sub
End Module
