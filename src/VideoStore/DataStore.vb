Imports Microsoft.Data.SqlClient

Public Class DataStore
    Public Property Connection As SqlConnection

    Public Sub New(connection As SqlConnection)
        Me.Connection = connection
    End Sub
End Class
