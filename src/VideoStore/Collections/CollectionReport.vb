Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module CollectionReport
    Friend Sub Run(store As DataStore)
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.CollectionReport
        Utility.LegacyExportHtml(
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
