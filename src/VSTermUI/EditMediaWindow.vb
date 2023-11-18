Imports Terminal.Gui

Friend Class EditMediaWindow
    Inherits Window
    Private ReadOnly store As VSData.DataStore
    Private ReadOnly mediaId As Integer
    Private ReadOnly titleTextField As TextField
    Private ReadOnly categoryComboBox As ComboBox
    Private categories As List(Of (Id As Integer, Abbr As String, Name As String))
    Private ReadOnly mediaTypeComboBox As ComboBox
    Private mediaTypes As List(Of (Id As Integer, Abbr As String, Name As String))
    Private ReadOnly collectionComboBox As ComboBox
    Private collections As List(Of (Id As Integer, Name As String))

    Public Sub New(store As VSData.DataStore, mediaId As Integer)
        MyBase.New("Edit Media...")
        Me.store = store
        Me.mediaId = mediaId
        Dim media = store.Media(mediaId).Value
        Dim titleLabel = New Label("     Title:") With
            {
                .X = 1,
                .Y = 1
            }
        titleTextField = New TextField(media.Title) With
            {
                .Y = titleLabel.Y,
                .X = Pos.Right(titleLabel) + 1,
                .Width = [Dim].Fill - 1
            }
        Dim categoryLabel = New Label("  Category:") With
            {
                .X = 1,
                .Y = Pos.Bottom(titleLabel) + 1
            }
        categoryComboBox = New ComboBox() With
            {
                .X = Pos.Right(categoryLabel) + 1,
                .Y = categoryLabel.Y,
                .Width = [Dim].Fill - 1
            }
        UpdateCategoryComboBox(media.CategoryId)
        Dim mediaTypeLabel = New Label("Media Type:") With
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
        UpdateMediaTypeComboBox(media.MediaTypeId)
        Dim collectionLabel = New Label("Collection:") With
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
        UpdateCollectionComboBox(media.CollectionId)
        Dim updateButton As New Button("Update") With
            {
                .X = 1,
                .Y = Pos.Bottom(collectionLabel) + 1
            }
        AddHandler updateButton.Clicked, AddressOf OnUpdateButtonClicked
        Add(
            titleLabel,
            titleTextField,
            categoryLabel,
            categoryComboBox,
            mediaTypeLabel,
            mediaTypeComboBox,
            collectionLabel,
            collectionComboBox,
            updateButton)
    End Sub

    Private Sub OnUpdateButtonClicked()
        If categoryComboBox.SelectedItem = -1 OrElse mediaTypeComboBox.SelectedItem = -1 Then
            Return
        End If
        Dim newTitle As String = titleTextField.Text.ToString
        Dim newCategoryId As Integer = categories(categoryComboBox.SelectedItem).Id
        Dim newMediaTypeId As Integer = mediaTypes(mediaTypeComboBox.SelectedItem).Id
        Dim newCollectionId As Integer? = If(collectionComboBox.SelectedItem = -1, Nothing, collections(collectionComboBox.SelectedItem).Id)
        store.UpdateMedia(mediaId, newTitle, newCategoryId, newMediaTypeId, newCollectionId)
        SuperView.Remove(Me)
    End Sub

    Private Sub UpdateCollectionComboBox(collectionId As Integer?)
        collections = store.CollectionList.ToList
        collectionComboBox.SetSource(collections.Select(AddressOf ToCollectionItem).ToList)
        collectionComboBox.SelectedItem = If(collectionId.HasValue, collections.FindIndex(Function(x) x.Id = collectionId.Value), -1)
    End Sub

    Private Sub UpdateMediaTypeComboBox(mediaTypeId As Integer)
        mediaTypes = store.MediaTypeList.ToList
        mediaTypeComboBox.SetSource(mediaTypes.Select(AddressOf ToMediaTypeItem).ToList)
        mediaTypeComboBox.SelectedItem = mediaTypes.FindIndex(Function(x) x.Id = mediaTypeId)
    End Sub

    Private Function ToMediaTypeItem(mediaTypeListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New MediaTypeListViewItem(mediaTypeListItem)
    End Function

    Private Sub UpdateCategoryComboBox(categoryId As Integer)
        categories = store.CategoryList.ToList
        categoryComboBox.SetSource(categories.Select(AddressOf ToCategoryItem).ToList)
        categoryComboBox.SelectedItem = categories.FindIndex(Function(x) x.Id = categoryId)
    End Sub
    Private Function ToCategoryItem(categoryListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New CategoryListViewItem(categoryListItem)
    End Function

    Private Function ToCollectionItem(collectionListItem As (Id As Integer, Name As String), arg2 As Integer) As Object
        Return New CollectionListViewItem(collectionListItem)
    End Function
End Class
