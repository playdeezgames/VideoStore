Imports Microsoft.Data.SqlClient
Imports VSData
Friend Module DeleteCollection
    Friend Sub Run(store As DataStore, collectionId As Integer)
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.CollectionDelete
        command.Parameters.AddWithValue(Parameters.CollectionId, collectionId)
        command.ExecuteNonQuery()
    End Sub
End Module
