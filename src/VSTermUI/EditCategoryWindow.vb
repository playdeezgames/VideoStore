Imports Terminal.Gui
Imports VSData

Friend Class EditCategoryWindow
    Inherits Window

    Private ReadOnly store As DataStore
    Private ReadOnly categoryId As Integer
    Private ReadOnly nameTextField As TextField
    Private ReadOnly abbrTextField As TextField
    Private ReadOnly originalAbbr As String
    Private ReadOnly originalName As String
    Private ReadOnly canDelete As Boolean

    Public Sub New(store As DataStore, categoryId As Integer)
        MyBase.New("Edit Category...")
        Me.store = store
        Me.categoryId = categoryId
        Dim category = store.Category(categoryId)
        Me.originalAbbr = category.Abbr
        Me.originalName = category.Name
        Me.canDelete = category.MediaCount = 0
        Dim nameLabel As New Label("Name:")
        nameTextField = New TextField With
            {
                .X = Pos.Right(nameLabel) + 1,
                .Text = category.Name,
                .Width = [Dim].Fill
            }
        Dim abbrLabel As New Label("Abbr:") With
            {
                .Y = Pos.Bottom(nameLabel) + 1
            }
        abbrTextField = New TextField With
            {
                .Text = category.Abbr,
                .Y = abbrLabel.Y,
                .X = Pos.Right(abbrLabel) + 1,
                .Width = [Dim].Fill
            }
        Dim doneButton As New Button("Done") With
            {
                .Y = Pos.Bottom(abbrLabel) + 1
            }
        AddHandler doneButton.Clicked, AddressOf OnDoneButtonClicked
        Dim cancelButton As New Button("Cancel") With
            {
                .Y = doneButton.Y,
                .X = Pos.Right(doneButton) + 1
            }
        AddHandler cancelButton.Clicked, AddressOf OnCancelButtonClicked
        Add(nameLabel, nameTextField, abbrLabel, abbrTextField, doneButton, cancelButton)
        If canDelete Then
            Dim deleteButton As New Button("Delete") With
                {
                    .Y = doneButton.Y,
                    .X = Pos.Right(cancelButton) + 1
                }
            AddHandler deleteButton.Clicked, AddressOf OnDeleteButtonClicked
            Add(deleteButton)
        End If
    End Sub

    Private Sub OnDeleteButtonClicked()
        store.DeleteCategory(categoryId)
        Application.RequestStop()
    End Sub

    Private Sub OnCancelButtonClicked()
        Application.RequestStop()
    End Sub

    Private Sub OnDoneButtonClicked()
        If originalAbbr <> abbrTextField.Text Then
            If store.CheckCategoryAbbreviation(abbrTextField.Text) Then
                MessageBox.ErrorQuery("Error!", "Duplicate Abbreviation!", "Ok")
                Return
            Else
                store.ChangeCategoryAbbreviation(categoryId, abbrTextField.Text)
            End If
        End If
        If originalName <> nameTextField.Text Then
            store.ChangeCategoryName(categoryId, nameTextField.Text)
        End If
        Application.RequestStop()
    End Sub
End Class
