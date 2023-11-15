Imports Microsoft.Data.SqlClient

Friend Module CollectionReport
    Friend Sub Run(connection As SqlConnection)
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CollectionReport
        Utility.ExportHtml(
            "Collection Report",
            "CollectionReport",
            New String() {
                "Collection Name",
                "Media Count"
            },
            command,
            New Func(Of SqlDataReader, Object)() {
                Function(reader) reader.GetString(0),
                Function(reader) reader.GetInt32(1)
            })
    End Sub
End Module
