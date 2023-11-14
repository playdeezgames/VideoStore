Imports Microsoft.Data.SqlClient

Friend Module NewCollection
    Friend Sub Run(connection As SqlConnection)
        Dim collectionName = AnsiConsole.Ask(Prompts.NewCollectionName, String.Empty)
        If String.IsNullOrWhiteSpace(collectionName) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CollectionInsert
        command.Parameters.AddWithValue(Parameters.CollectionName, collectionName)
        command.ExecuteNonQuery()
    End Sub
End Module
