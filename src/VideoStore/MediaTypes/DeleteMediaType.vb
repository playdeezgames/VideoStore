Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module DeleteMediaType
    Friend Sub Run(store As DataStore, mediaTypeId As Integer)
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.MediaTypeDelete
        command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
        command.ExecuteNonQuery()
    End Sub
End Module
