Imports Terminal.Gui
Imports VSData

Friend Class CategoriesWindow
    Inherits Window

    Private ReadOnly store As DataStore
    Private ReadOnly categoryListView As ListView

    Public Sub New(store As DataStore)
        MyBase.New("Categories...")
        Me.store = store
        Dim goBackButton = New Button() With
            {
                .Text = "Go Back"
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Dim addButton = New Button() With
            {
                .Text = "Add...",
                .X = Pos.Right(goBackButton) + 1
            }
        AddHandler addButton.Clicked, AddressOf AddButtonClicked
        categoryListView = New ListView() With
            {
                .Y = Pos.Bottom(addButton) + 1,
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        UpdateCategoryListView()
        AddHandler categoryListView.OpenSelectedItem, AddressOf OnCategoryListViewOpenSelectedItem
        Add(goBackButton, addButton, categoryListView)
    End Sub

    Private Sub UpdateCategoryListView()
        categoryListView.SetSource(store.CategoryList.Select(AddressOf ToListViewItem).ToList)
    End Sub

    Private Sub OnCategoryListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim item = CType(args.Value, CategoryListViewItem).CategoryListItem
        Application.Run(New EditCategoryWindow(store, item.Id))
        UpdateCategoryListView()
    End Sub

    Private Function ToListViewItem(categoryListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New CategoryListViewItem(categoryListItem)
    End Function

    Private Sub AddButtonClicked()
        Application.Run(New AddCategoryWindow(store))
        UpdateCategoryListView()
    End Sub

    Private Sub OnGoBackButtonClicked()
        Application.RequestStop()
    End Sub
End Class
