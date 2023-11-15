Imports Microsoft.Data.SqlClient

Friend Module NewMediaType
    Friend Sub Run(store As DataStore)
        Dim name = AnsiConsole.Ask(Prompts.NewMediaTypeName, String.Empty)
        If String.IsNullOrEmpty(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(Prompts.NewMediaTypeAbbreviation, String.Empty)
        If String.IsNullOrEmpty(abbreviation) Then
            Return
        End If
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.MediaTypeCheckAbbreviation
        command.Parameters.AddWithValue(Parameters.MediaTypeAbbr, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(Errors.DuplicateAbbreviation)
            Return
        End If
        command = store.Connection.CreateCommand
        command.CommandText = Commands.MediaTypeInsert
        command.Parameters.AddWithValue(Parameters.MediaTypeAbbr, abbreviation)
        command.Parameters.AddWithValue(Parameters.MediaTypeName, name)
        command.ExecuteNonQuery()
    End Sub
End Module
