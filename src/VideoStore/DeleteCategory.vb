Imports Microsoft.Data.SqlClient

Friend Module DeleteCategory
    Private Const CategoryIdParameterName As String = "@CategoryId"
    Friend Sub DeleteCategory(connection As SqlConnection, categoryId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = "DELETE FROM Categories WHERE CategoryId=@CategoryId;"
        command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
        command.ExecuteNonQuery()
    End Sub
End Module
