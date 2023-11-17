Friend Class CategoryListViewItem
    Public ReadOnly Property CategoryListItem As (Id As Integer, Abbr As String, Name As String)
    Sub New(categoryListItem As (Id As Integer, Abbr As String, Name As String))
        Me.categoryListItem = categoryListItem
    End Sub
    Public Overrides Function ToString() As String
        Return $"{categoryListItem.Abbr} - {categoryListItem.Name}"
    End Function
End Class
