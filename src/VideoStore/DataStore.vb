Imports Microsoft.Data.SqlClient

Public Class DataStore
    Public Property Connection As SqlConnection
    Public ReadOnly Property CategoryReport As IEnumerable(Of (Id As Integer, Abbr As String, Name As String, MediaCount As Integer))
        Get
            Dim result As New List(Of (Id As Integer, Abbr As String, Name As String, MediaCount As Integer))
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.CategoryReport
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)))
                End While
            End Using
            Return result
        End Get
    End Property
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

    Friend ReadOnly Property Category(categoryId As Integer) As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer)
        Get
            Dim command = Connection.CreateCommand()
            command.CommandText = CategoryDetails
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            Using reader = command.ExecuteReader()
                reader.Read()
                Return (categoryId, reader.GetString(0), reader.GetString(1), reader.GetInt32(2))
            End Using
        End Get
    End Property

    Public Sub New(connection As SqlConnection)
        Me.Connection = connection
    End Sub

    Friend Sub ChangeCategoryAbbreviation(categoryId As Integer, newAbbreviation As String)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryUpdateAbbreviation
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, newAbbreviation)
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Friend Sub ChangeCategoryName(categoryId As Integer, newName As String)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryUpdateName
        command.Parameters.AddWithValue(Parameters.CategoryName, newName)
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Friend Sub DeleteCategory(categoryId As Integer)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryDelete
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Friend Function CheckCategoryAbbreviation(abbreviation As String) As Boolean
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviation
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        Return result > 0
    End Function

    Friend Sub CreateCategory(name As String, abbreviation As String)
        Dim Command = Connection.CreateCommand
        Command.CommandText = CategoryInsert
        Command.Parameters.AddWithValue(Parameters.CategoryName, name)
        Command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        Command.ExecuteNonQuery()
    End Sub
End Class
