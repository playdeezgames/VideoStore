Imports Microsoft.Data.SqlClient
Friend Module DeleteCollection
    Friend Sub Run(connection As SqlConnection, collectionId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CollectionDelete
        command.Parameters.AddWithValue(Parameters.CollectionId, collectionId)
        command.ExecuteNonQuery()
    End Sub
End Module
