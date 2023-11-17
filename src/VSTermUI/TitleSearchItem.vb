Public Class TitleSearchItem
    Public ReadOnly Property TitleSearchItem As (Id As Integer, Title As String, Category As String, MediaType As String, Collection As String)
    Sub New(titleSearchItem As (Id As Integer, Title As String, Category As String, MediaType As String, Collection As String))
        Me.TitleSearchItem = titleSearchItem
    End Sub
    Public Overrides Function ToString() As String
        Return $"{TitleSearchItem.Title}"
    End Function

End Class
