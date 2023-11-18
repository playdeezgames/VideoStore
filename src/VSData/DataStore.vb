Imports System.Reflection
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
    Public ReadOnly Property MediaTypeMediaList(mediaTypeId As Integer) As IEnumerable(Of (Id As Integer, Title As String, Category As String, Collection As String))
        Get
            Dim results As New List(Of (Id As Integer, Title As String, Category As String, Collection As String))
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.MediaTypeMediaList
            command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
            Using reader = command.ExecuteReader
                While reader.Read
                    results.Add((
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                If(reader.IsDBNull(3), Nothing, reader.GetString(3))))
                End While
            End Using
            Return results
        End Get
    End Property
    Public ReadOnly Property MediaTypeList As IEnumerable(Of (Id As Integer, Abbr As String, Name As String))
        Get
            Dim results As New List(Of (Id As Integer, Abbr As String, Name As String))
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.MediaTypeList
            Using reader = command.ExecuteReader
                While reader.Read
                    results.Add((reader.GetInt32(0), reader.GetString(2), reader.GetString(1)))
                End While
            End Using
            Return results
        End Get
    End Property

    Public ReadOnly Property Category(categoryId As Integer) As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer)
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

    Public Sub ChangeCategoryAbbreviation(categoryId As Integer, newAbbreviation As String)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryUpdateAbbreviation
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, newAbbreviation)
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Public Sub ChangeCategoryName(categoryId As Integer, newName As String)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryUpdateName
        command.Parameters.AddWithValue(Parameters.CategoryName, newName)
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Public Sub DeleteCategory(categoryId As Integer)
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryDelete
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        command.ExecuteNonQuery()
    End Sub

    Public Function CheckCategoryAbbreviation(abbreviation As String) As Boolean
        Dim command = Connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviation
        command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        Return result > 0
    End Function

    Public Sub CreateCategory(name As String, abbreviation As String)
        Dim Command = Connection.CreateCommand
        Command.CommandText = CategoryInsert
        Command.Parameters.AddWithValue(Parameters.CategoryName, name)
        Command.Parameters.AddWithValue(Parameters.CategoryAbbr, abbreviation)
        Command.ExecuteNonQuery()
    End Sub
    Public ReadOnly Property MediaReport As IEnumerable(Of (MediaTitle As String, Category As String, MediaType As String, Collection As String))
        Get
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.MediaReport
            Dim results As New List(Of (MediaTitle As String, Category As String, MediaType As String, Collection As String))
            Using reader = command.ExecuteReader
                While reader.Read
                    results.Add((
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                If(reader.IsDBNull(3), String.Empty, reader.GetString(3))))
                End While
            End Using
            Return results
        End Get
    End Property
    Public Sub AddMedia(mediaTitle As String, categoryId As Integer, mediaTypeId As Integer, collectionId As Integer?)
        Dim command = Connection.CreateCommand
        command.CommandText = If(collectionId.HasValue, Commands.MediaInsertWithCollection, Commands.MediaInsert)
        command.Parameters.AddWithValue(Parameters.MediaTitle, mediaTitle)
        command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
        command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
        If collectionId.HasValue Then
            command.Parameters.AddWithValue(Parameters.CollectionId, collectionId.Value)
        End If
        command.ExecuteNonQuery()
    End Sub
    Public ReadOnly Property CollectionList As IEnumerable(Of (Id As Integer, Name As String))
        Get
            Dim result As New List(Of (Id As Integer, Name As String))
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.CollectionList
            command.Parameters.AddWithValue(Parameters.NameFilter, "%")
            Using reader = command.ExecuteReader()
                While reader.Read
                    result.Add((reader.GetInt32(0), reader.GetString(1)))
                End While
            End Using
            Return result
        End Get
    End Property
    Public ReadOnly Property TitleSearch(filter As String) As IEnumerable(Of (Id As Integer, Title As String, Category As String, MediaType As String, Collection As String))
        Get
            Dim result As New List(Of (Id As Integer, Title As String, Category As String, MediaType As String, Collection As String))
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.TitleSearch
            command.Parameters.AddWithValue(Parameters.NameFilter, filter)
            Using reader = command.ExecuteReader()
                While reader.Read
                    result.Add((
                               reader.GetInt32(0),
                               reader.GetString(1),
                               reader.GetString(2),
                               reader.GetString(3),
                               If(reader.IsDBNull(4), Nothing, reader.GetString(4))))
                End While
            End Using
            Return result
        End Get
    End Property
    Public ReadOnly Property Media(mediaId As Integer) As (Id As Integer, Title As String, CategoryId As Integer)?
        Get
            Dim command = Connection.CreateCommand
            command.CommandText = Commands.MediaById
            command.Parameters.AddWithValue(Parameters.MediaId, mediaId)
            Using reader = command.ExecuteReader
                If reader.Read Then
                    Return (reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2))
                End If
            End Using
            Return Nothing
        End Get
    End Property

End Class
