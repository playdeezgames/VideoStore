Friend Module CategoryReport
    Friend Sub Run(store As DataStore)
        Utility.ExportHtml(
            "Category Report",
            "CategoryReport",
            New String() {
                "Category Name",
                "Abbreviation",
                "Media Count"
            },
            store.CategoryReport,
            New Func(Of (Id As Integer, Abbr As String, Name As String, MediaCount As Integer), Object)() {
                Function(item) item.Name,
                Function(item) item.Abbr,
                Function(item) item.MediaCount
            })
    End Sub
End Module
