Imports Microsoft.Data.SqlClient
Friend Module CategoryReport
    Friend Sub Run(store As DataStore)
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.CategoryReport
        Utility.ExportHtml(
            "Category Report",
            "CategoryReport",
            New String() {
                "Category Name",
                "Abbreviation",
                "Media Count"
            },
            command,
            New Func(Of SqlDataReader, Object)() {
                Function(reader) reader.GetString(0),
                Function(reader) reader.GetString(1),
                Function(reader) reader.GetInt32(2)
            })
    End Sub
End Module
