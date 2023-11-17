Imports Microsoft.Data.SqlClient
Imports Terminal.Gui
Module Program
    Sub Main(args As String())
        Console.Title = "Video Store Terminal UI"
        Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=MediaLibrary;Integrated Security=true;TrustServerCertificate=true")
            connection.Open()
            Application.Init()
            Application.Run(New MainWindow(New VSData.DataStore(connection)))
            Application.Shutdown()
            connection.Close()
        End Using
    End Sub
End Module
