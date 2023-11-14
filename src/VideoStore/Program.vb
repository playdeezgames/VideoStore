Imports Microsoft.Data.SqlClient

Module Program
    Sub Main(args As String())
        Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=MediaLibrary;Integrated Security=true;TrustServerCertificate=true")
            connection.Open()
            MainMenu.Run(connection)
            connection.Close()
        End Using
    End Sub
End Module
