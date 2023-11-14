Imports Microsoft.Data.SqlClient

Friend Module DeleteCategory
    Private Const CategoryIdParameterName As String = "@CategoryId"

    Friend Sub DeleteCategory(connection As SqlConnection, categoryId As Integer)
        Dim command = connection.CreateCommand
        command.CommandText = CategoryDelete
        command.Parameters.AddWithValue(CategoryIdParameterName, categoryId)
        command.ExecuteNonQuery()
    End Sub
End Module
