Public Class CollectionListViewItem
    Public ReadOnly Property CollectionListItem As (Id As Integer, Name As String)
    Sub New(categoryListItem As (Id As Integer, Name As String))
        Me.CollectionListItem = categoryListItem
    End Sub
    Public Overrides Function ToString() As String
        Return $"{CollectionListItem.Name}"
    End Function
End Class
