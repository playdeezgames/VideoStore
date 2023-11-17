Imports Terminal.Gui
Imports VSData

Friend Class CategoriesWindow
    Inherits Window

    Private ReadOnly store As DataStore

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
                .Y = Pos.Bottom(goBackButton) + 1
            }
        AddHandler addButton.Clicked, AddressOf AddButtonClicked
        Dim categoryList = New ListView(store.CategoryList.Select(AddressOf ToListViewItem).ToList) With
            {
                .Y = Pos.Bottom(addButton) + 1,
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler categoryList.OpenSelectedItem, AddressOf OnCategoryListViewOpenSelectedItem
        Add(goBackButton, addButton, categoryList)
    End Sub

    Private Sub OnCategoryListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim categoryId = CType(args.Value, CategoryListViewItem).CategoryListItem.Id
        MessageBox.Query("!", $"{categoryId}", "Ok")
    End Sub

    Private Function ToListViewItem(categoryListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New CategoryListViewItem(categoryListItem)
    End Function

    Private Sub AddButtonClicked()
        Throw New NotImplementedException()
    End Sub

    Private Sub OnGoBackButtonClicked()
        Application.RequestStop()
    End Sub
End Class
