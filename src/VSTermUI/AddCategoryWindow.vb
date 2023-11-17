Imports Terminal.Gui
Imports VSData

Friend Class AddCategoryWindow
    Inherits Window

    Private ReadOnly store As DataStore
    Private ReadOnly nameTextField As TextField
    Private ReadOnly abbrTextField As TextField

    Public Sub New(store As DataStore)
        MyBase.New("Add Category...")
        Me.store = store
        Dim nameLabel As New Label("Name:")
        nameTextField = New TextField With
            {
                .X = Pos.Right(nameLabel) + 1,
                .Text = "Category Name",
                .Width = [Dim].Fill
            }
        Dim abbrLabel As New Label("Abbr:") With
            {
                .Y = Pos.Bottom(nameLabel) + 1
            }
        abbrTextField = New TextField With
            {
                .Text = "ABBR",
                .Y = abbrLabel.Y,
                .X = Pos.Right(abbrLabel) + 1,
                .Width = [Dim].Fill
            }
        Dim okButton As New Button("Ok") With
            {
                .Y = Pos.Bottom(abbrLabel) + 1
            }
        AddHandler okButton.Clicked, AddressOf OnOkButtonClicked
        Dim cancelButton As New Button("Cancel") With
            {
                .Y = okButton.Y,
                .X = Pos.Right(okButton) + 1
            }
        AddHandler cancelButton.Clicked, AddressOf OnCancelButtonClicked
        Add(nameLabel, nameTextField, abbrLabel, abbrTextField, okButton, cancelButton)
    End Sub

    Private Sub OnCancelButtonClicked()
        Application.RequestStop()
    End Sub

    Private Sub OnOkButtonClicked()
        If store.CheckCategoryAbbreviation(abbrTextField.Text) Then
            MessageBox.ErrorQuery("Error!", "Duplicate Abbreviation!", "Ok")
        Else
            store.CreateCategory(nameTextField.Text, abbrTextField.Text)
            Application.RequestStop()
        End If
    End Sub
End Class
