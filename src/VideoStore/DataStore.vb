Imports Microsoft.Data.SqlClient
Imports Microsoft.Identity.Client

Public Class DataStore
    Public Property Connection As SqlConnection
    Public ReadOnly Property CategoryList As IEnumerable(Of (Id As Integer, Abbr As String, Name As String))
        Get
            Dim result As New List(Of (Id As Integer, Abbr As String, Name As String))
            Using command = Connection.CreateCommand
                command.CommandText = Commands.CategoryList
                Using reader = command.ExecuteReader
                    While reader.Read
                        Dim item As (Id As Integer, Abbr As String, Name As String) = (reader.GetInt32(0), reader.GetString(1), reader.GetString(2))
                        result.Add(item)
                    End While
                End Using
            End Using
            Return result
        End Get
    End Property

    Public Sub New(connection As SqlConnection)
        Me.Connection = connection
    End Sub
End Class
