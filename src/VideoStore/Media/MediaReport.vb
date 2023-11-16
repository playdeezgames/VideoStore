Imports VSData

Friend Module MediaReport
    Friend Sub Run(store As DataStore)
        Utility.ExportHtml(
            "Media Report",
            "MediaReport",
            New String() {
                "Media Title",
                "Category",
                "Media Type",
                "Collection"
            },
            store.MediaReport,
            New Func(Of (MediaTitle As String, Category As String, MediaType As String, Collection As String), Object)() {
                Function(item) item.MediaTitle,
                Function(item) item.Category,
                Function(item) item.MediaType,
                Function(item) item.Collection
            })
    End Sub
End Module
