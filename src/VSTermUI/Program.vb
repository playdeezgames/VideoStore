Imports Microsoft.Data.SqlClient
Imports Terminal.Gui
Module Program
    Sub Main(args As String())
        Console.Title = "Video Store Terminal UI"
        Dim window As Window = Nothing
        Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=MediaLibrary;Integrated Security=true;TrustServerCertificate=true")
            connection.Open()
            Application.Init()
            Dim store = New VSData.DataStore(connection)
            Application.Top.Add(
                New MenuBar(
                New MenuBarItem() {
                    New MenuBarItem(
                        "_Media",
                        New MenuItem() {
                            New MenuItem(
                                "_Intake...",
                                String.Empty,
                                Sub()
                                    If window IsNot Nothing Then
                                        Application.Top.Remove(window)
                                        window = Nothing
                                    End If
                                    window = New AddMediaWindow(store)
                                    Application.Top.Add(window)
                                End Sub),
                            New MenuItem(
                                "_Search...",
                                String.Empty,
                                Sub()
                                    If window IsNot Nothing Then
                                        Application.Top.Remove(window)
                                        window = Nothing
                                    End If
                                    window = New TitleSearchWindow(store)
                                    Application.Top.Add(window)
                                End Sub)})}))
            Application.Run()
            Application.Shutdown()
            connection.Close()
        End Using
    End Sub
End Module
