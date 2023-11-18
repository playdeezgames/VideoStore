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
        Dim collectionLabel = New Label("Collection:") With
            {
                .X = 1,
                .Y = Pos.Bottom(mediaTypeLabel) + 1
            }
        Add(titleLabel, titleTextField, categoryLabel, categoryComboBox, mediaTypeLabel, collectionLabel)
    End Sub
    Private Sub UpdateCategoryComboBox(categoryId As Integer)
        categories = store.CategoryList.ToList
        categoryComboBox.SetSource(categories.Select(AddressOf ToCategoryItem).ToList)
        categoryComboBox.SelectedItem = categories.FindIndex(Function(x) x.Id = categoryId)
    End Sub
    Private Function ToCategoryItem(categoryListItem As (Id As Integer, Abbr As String, Name As String), arg2 As Integer) As Object
        Return New CategoryListViewItem(categoryListItem)
    End Function
End Class
