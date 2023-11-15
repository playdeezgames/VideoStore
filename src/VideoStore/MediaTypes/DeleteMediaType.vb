Imports Microsoft.Data.SqlClient

Friend Module DeleteMediaType
    Friend Sub Run(connection As SqlConnection, mediaTypeId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = Commands.MediaTypeDelete
        command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
        command.ExecuteNonQuery()
    End Sub
End Module
