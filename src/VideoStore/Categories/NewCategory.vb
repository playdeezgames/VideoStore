Imports Microsoft.Data.SqlClient

Friend Module NewCategory
    Friend Sub Run(store As DataStore)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryName, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviation, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = store.Connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviation
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(DuplicateAbbreviation)
            OkPrompt()
            Return
        End If
        command = store.Connection.CreateCommand
        command.CommandText = CategoryInsert
        command.Parameters.AddWithValue(Parameters.CategoryName, name)
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        command.ExecuteNonQuery()
    End Sub
End Module
