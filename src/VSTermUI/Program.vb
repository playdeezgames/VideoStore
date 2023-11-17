Imports Microsoft.Data.SqlClient
Imports Terminal.Gui
Module Program
    Sub Main(args As String())
        Console.Title = "Video Store Terminal UI"
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
                                    If Application.Top.Subviews.Any(Function(x) x.Text = "Add Media...") Then
                                        Application.Top.Subviews.Single(Function(x) x.Text = "Add Media...").SetFocus()
                                    Else
                                        Application.Top.Add(New AddMediaWindow(store))
                                    End If
                                End Sub),
                            New MenuItem(
                                "_Search...",
                                String.Empty,
                                Sub()
                                    If Application.Top.Subviews.Any(Function(x) x.Text = "Title Search...") Then
                                        Application.Top.Subviews.Single(Function(x) x.Text = "Title Search...").SetFocus()
                                    Else
                                        Application.Top.Add(New TitleSearchWindow(store))
                                    End If
                                End Sub)})}))
            Application.Run()
            Application.Shutdown()
            connection.Close()
        End Using
    End Sub
End Module
