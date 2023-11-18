Imports Microsoft.Data.SqlClient
Imports Terminal.Gui
Imports VSData
Module Program
    Public window As Window = Nothing
    Public store As DataStore = Nothing
    Sub Main(args As String())
        Console.Title = "Video Store Terminal UI"
        Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=MediaLibrary;Integrated Security=true;TrustServerCertificate=true")
            connection.Open()
            Application.Init()
            store = New VSData.DataStore(connection)
            Application.Top.Add(
                New MenuBar(
                New MenuBarItem() {
                    New MenuBarItem(
                        "_Media",
                        New MenuItem() {
                            New MenuItem(
                                "_Intake...",
                                String.Empty,
                                AddressOf ShowAddMediaWindow),
                            New MenuItem(
                                "_Search...",
                                String.Empty,
                                AddressOf ShowTitleSearchWindow)})}))
            Application.Run()
            Application.Shutdown()
            connection.Close()
        End Using
    End Sub
    Sub GoToWindow(newWindow As Window)
        If window IsNot Nothing Then
            Application.Top.Remove(window)
            window = Nothing
        End If
        window = newWindow
        Application.Top.Add(window)
    End Sub
    Sub ShowAddMediaWindow()
        GoToWindow(New AddMediaWindow(store))
    End Sub
    Sub ShowTitleSearchWindow()
        GoToWindow(New TitleSearchWindow(store))
    End Sub
End Module
