Imports Terminal.Gui
Imports VSData

Friend Class MainWindow
    Inherits Window
    Private ReadOnly store As DataStore
    Sub New(store As DataStore)
        MyBase.New("Video Store")
        Me.store = store
        Dim categoriesButton As New Button() With
            {
                .Text = "_Categories..."
            }
        AddHandler categoriesButton.Clicked, AddressOf OnCategoriesButtonClicked
        Dim mediaTypesButton As New Button() With
            {
                .Text = "Media _Types...",
                .Y = Pos.Bottom(categoriesButton) + 1
            }
        AddHandler mediaTypesButton.Clicked, AddressOf OnMediaTypesButtonClicked
        Dim collectionsButton As New Button() With
            {
                .Text = "C_ollections...",
                .Y = Pos.Bottom(mediaTypesButton) + 1
            }
        AddHandler collectionsButton.Clicked, AddressOf OnCollectionsButtonClicked
        Dim mediaButton As New Button() With
            {
                .Text = "_Media...",
                .Y = Pos.Bottom(collectionsButton) + 1
            }
        AddHandler mediaButton.Clicked, AddressOf OnMediaButtonClicked
        Add(categoriesButton, mediaTypesButton, collectionsButton, mediaButton)
    End Sub

    Private Sub OnMediaButtonClicked()
        Throw New NotImplementedException()
    End Sub

    Private Sub OnCollectionsButtonClicked()
        Throw New NotImplementedException()
    End Sub

    Private Sub OnMediaTypesButtonClicked()
        Throw New NotImplementedException()
    End Sub

    Private Function OnCategoriesButtonClicked() As Object
        Application.Run(New CategoriesWindow(store))
    End Function
End Class
