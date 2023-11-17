Imports Terminal.Gui
Imports VSData

Friend Class MediaWindow
    Inherits Window

    Private ReadOnly store As DataStore

    Public Sub New(store As DataStore)
        MyBase.New("Media")
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
        Add(goBackButton, addButton)
    End Sub

    Private Sub AddButtonClicked()
        Application.Run(New AddMediaWindow(store))
    End Sub

    Private Sub OnGoBackButtonClicked()
        Application.RequestStop()
    End Sub
End Class
