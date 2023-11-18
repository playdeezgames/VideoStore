Imports NStack
Imports Terminal.Gui
Imports VSData

Friend Class TitleSearchWindow
    Inherits Window

    Private ReadOnly store As DataStore
    Private ReadOnly filterTextField As TextField
    Private ReadOnly resultsListView As ListView

    Public Sub New(store As DataStore)
        MyBase.New("Title Search...")
        Me.store = store
        Dim filterLabel As New Label("Filter:") With
            {
                .X = 1,
                .Y = 1
            }
        filterTextField = New TextField() With
            {
                .X = Pos.Right(filterLabel) + 1,
                .Y = filterLabel.Y,
                .Width = [Dim].Fill - 1
            }
        AddHandler filterTextField.TextChanged, AddressOf OnFilterTextFieldTextChanged
        resultsListView = New ListView() With
            {
                .X = 1,
                .Y = Pos.Bottom(filterLabel) + 1,
                .Width = [Dim].Fill - 1,
                .Height = [Dim].Fill - 1
            }
        AddHandler resultsListView.OpenSelectedItem, AddressOf OnResultsListViewOpenSelectedItem
        UpdateResultsListView()
        Add(filterLabel, filterTextField, resultsListView)
    End Sub

    Private Sub OnResultsListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim mediaId = CType(args.Value, TitleSearchItem).TitleSearchItem.Id
        Program.GoToWindow(New EditMediaWindow(Program.store, mediaId))
    End Sub

    Private Sub OnFilterTextFieldTextChanged(text As ustring)
        UpdateResultsListView()
    End Sub

    Private Sub UpdateResultsListView()
        Dim filter = "%"
        If filterTextField.Text.ToString.Any Then
            filter += filterTextField.Text.ToString
            filter += "%"
        End If
        Dim items = store.TitleSearch(filter)
        resultsListView.SetSource(items.Select(AddressOf ToTitleSearchItem).ToList)
    End Sub

    Private Function ToTitleSearchItem(arg1 As (Id As Integer, Title As String, Category As String, MediaType As String, Collection As String), arg2 As Integer) As Object
        Return New TitleSearchItem(arg1)
    End Function
End Class
