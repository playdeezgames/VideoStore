Imports Microsoft.Data.SqlClient

Friend Module DeleteCategory
    Friend Sub DeleteCategory(connection As SqlConnection, categoryId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = CategoryDelete
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub
End Module
