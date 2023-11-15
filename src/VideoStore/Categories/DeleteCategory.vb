Imports Microsoft.Data.SqlClient

Friend Module DeleteCategory
    Friend Sub Run(store As DataStore, categoryId As Integer)
        Dim command = store.Connection.CreateCommand
        command.CommandText = CategoryDelete
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub
End Module
