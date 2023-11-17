Imports Terminal.Gui
Imports VSData

Friend Class AddMediaWindow
    Inherits Window
    Private ReadOnly store As DataStore
    Private ReadOnly titleTextField As TextField
    Private ReadOnly categoryComboBox As ComboBox
    Private categories As List(Of (Id As Integer, Abbr As String, Name As String))
    Private ReadOnly mediaTypeComboBox As ComboBox
    Private mediaTypes As List(Of (Id As Integer, Abbr As String, Name As String))
    Private ReadOnly collectionComboBox As ComboBox
    Private collections As List(Of (Id As Integer, Name As String))
    Private ReadOnly recentListView As ListView
    Private ReadOnly recentlyAddedItems As New List(Of String)

    Public Sub New(store As DataStore)
        MyBase.New("Add Media...")
        Me.store = store
        Dim titleLabel As New Label("Title:") With
            {
                .X = 1,
                .Y = 1
            }
        titleTextField = New TextField With
            {
                .X = Pos.Right(titleLabel) + 1,
                .Y = titleLabel.Y,
                .Width = [Dim].Fill - 1
            }
        Dim categoryLabel As New Label("Category:") With
            {
                .X = 1,
                .Y = Pos.Bottom(titleTextField) + 1
            }
        categoryComboBox = New ComboBox() With
            {
                .X = Pos.Right(categoryLabel) + 1,
                .Y = categoryLabel.Y,
                .Width = [Dim].Fill - 1
            }
        UpdateCategoryComboBox()
        Dim mediaTypeLabel As New Label("Media Type:") With
            {
                .X = 1,
                .Y = Pos.Bottom(categoryLabel) + 1
            }
        mediaTypeComboBox = New ComboBox With
            {
                .Y = mediaTypeLabel.Y,
                .X = Pos.Right(mediaTypeLabel) + 1,
                .Width = [Dim].Fill - 1
            }
        UpdateMediaTypeComboBox()
        Dim collectionLabel As New Label("Collection:") With
            {
                .X = 1,
                .Y = Pos.Bottom(mediaTypeLabel) + 1
            }
        collectionComboBox = New ComboBox With
            {
                .Y = collectionLabel.Y,
                .X = Pos.Right(collectionLabel) + 1,
                .Width = [Dim].Fill - 1
            }
        UpdateCollectionComboBox()
        Dim addButton As New Button("Add") With
            {
                .X = 1,
                .Y = Pos.Bottom(collectionLabel) + 1
            }
        AddHandler addButton.Clicked, AddressOf OnAddButtonClicked
        Dim cancelButton As New Button("Cancel") With
            {
                .Y = addButton.Y,
                .X = Pos.Right(addButton) + 1
            }
        AddHandler cancelButton.Clicked, AddressOf OnCancelButtonClicked
        Dim recentlyAddedLabel As New Label("Recently Added:") With
            {
                .X = 1,
                .Y = Pos.Bottom(addButton) + 1
            }
        recentListView = New ListView With
            {
                .X = 1,
                .Y = Pos.Bottom(recentlyAddedLabel) + 1,
                .Width = [Dim].Fill - 1,
                .Height = [Dim].Fill,
                .CanFocus = False
            }
        UpdateRecentListView()
        Add(
            titleLabel,
            titleTextField,
            categoryLabel,
            categoryComboBox,
            mediaTypeLabel,
            mediaTypeComboBox,
            collectionLabel,
            collectionComboBox,
            addButton,
            cancelButton,
            recentlyAddedLabel,
            recentListView)
        titleTextField.SetFocus()
    End Sub

    Private Sub UpdateRecentListView()
        recentListView.SetSource(recentlyAddedItems)
    End Sub

    Private Sub UpdateCollectionComboBox()
        collections = store.CollectionList
        collectionComboBox.SetSource(collections.Select(AddressOf ToCollectionItem).ToList)
    End Sub

    Private Function ToCollectionItem(collectionListItem As (Id As Integer, Name As String), arg2 As Integer) As Object
        Return New CollectionListViewItem(collectionListItem)
    End Function

    Private Sub UpdateMediaTypeComboBox()
        mediaTypes = store.MediaTypeList
        mediaTypeComboBox.SetSource(mediaTypes.Select(AddressOf ToMediaTypeItem).ToList)
    End Sub

    Private Function ToMediaTypeItem(mediaTypeListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New MediaTypeListViewItem(mediaTypeListItem)
    End Function

    Private Sub UpdateCategoryComboBox()
        categories = store.CategoryList
        categoryComboBox.SetSource(categories.Select(AddressOf ToCategoryItem).ToList)
    End Sub


    Private Function ToCategoryItem(categoryListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New CategoryListViewItem(categoryListItem)
    End Function

    Private Sub OnAddButtonClicked()
        If String.IsNullOrEmpty(titleTextField.Text) Then
            Return
        End If
        If categoryComboBox.SelectedItem = -1 Then
            Return
        End If
        If mediaTypeComboBox.SelectedItem = -1 Then
            Return
        End If
        Dim collectionId As Integer? = Nothing
        If collectionComboBox.SelectedItem > -1 Then
            collectionId = collections(collectionComboBox.SelectedItem).Id
        End If
        recentlyAddedItems.Insert(0, titleTextField.Text)
        store.AddMedia(titleTextField.Text, categories(categoryComboBox.SelectedItem).Id, mediaTypes(mediaTypeComboBox.SelectedItem).Id, collectionId)
        titleTextField.Text = String.Empty
        categoryComboBox.SelectedItem = -1
        mediaTypeComboBox.SelectedItem = -1
        collectionComboBox.SelectedItem = -1
        titleTextField.SetFocus()
    End Sub

    Private Sub OnCancelButtonClicked()
        Application.RequestStop()
    End Sub
End Class
