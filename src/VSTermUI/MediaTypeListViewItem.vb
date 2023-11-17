Public Class MediaTypeListViewItem
    Public ReadOnly Property MediaTypeListItem As (Id As Integer, Abbr As String, Name As String)
    Sub New(categoryListItem As (Id As Integer, Abbr As String, Name As String))
        Me.MediaTypeListItem = categoryListItem
    End Sub
    Public Overrides Function ToString() As String
        Return $"{MediaTypeListItem.Abbr} - {MediaTypeListItem.Name}"
    End Function

End Class
