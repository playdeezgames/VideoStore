Imports Microsoft.Data.SqlClient
Imports Terminal.Gui
Imports VSData
Module Program
    Public window As Window = Nothing
    Public windowStack As New Stack(Of Window)
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
                                AddressOf ShowTitleSearchWindow)}),
                    New MenuBarItem("_Reports",
                        New MenuItem() {
                            New MenuItem(
                                "_Inventory",
                                String.Empty,
                                AddressOf RunInventoryReport)})}))
            Application.Run()
            Application.Shutdown()
            connection.Close()
        End Using
    End Sub

    Private Sub RunInventoryReport()
        InventoryReport.Run(store)
    End Sub

    Sub GoToWindow(newWindow As Window)
        If window IsNot Nothing Then
            Application.Top.Remove(window)
            window = Nothing
        End If
        window = newWindow
        Application.Top.Add(window)
    End Sub
    Sub PushWindow(newWindow As Window)
        If window IsNot Nothing Then
            windowStack.Push(window)
        End If
        GoToWindow(newWindow)
    End Sub
    Sub PopWindow()
        GoToWindow(windowStack.Pop)
    End Sub
    Sub ShowAddMediaWindow()
        GoToWindow(New AddMediaWindow(store))
    End Sub
    Sub ShowTitleSearchWindow()
        GoToWindow(New TitleSearchWindow(store))
    End Sub
End Module
