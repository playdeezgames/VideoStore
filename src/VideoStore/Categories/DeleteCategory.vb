Imports Microsoft.Data.SqlClient

Friend Module DeleteCategory
    Friend Sub Run(store As DataStore, categoryId As Integer)
        store.DeleteCategory(categoryId)
    End Sub
End Module
