Imports Microsoft.Data.SqlClient

Friend Module NewMedia
    Friend Sub Run(connection As SqlConnection)
        Do
            Dim mediaTypeId As Integer = PickMediaType(connection)
            Dim categoryId As Integer = PickCategory(connection)
            Dim collectionId As Integer? = PickCollection(connection)
            Dim title = AnsiConsole.Ask(Prompts.NewMediaTitle, String.Empty)
            If String.IsNullOrWhiteSpace(title) Then
                Exit Do
            End If
            Dim command = connection.CreateCommand
            command.CommandText = If(collectionId.HasValue, Commands.MediaInsertWithCollection, Commands.MediaInsert)
            command.Parameters.AddWithValue(Parameters.MediaTitle, title)
            command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            If collectionId.HasValue Then
                command.Parameters.AddWithValue(Parameters.CollectionId, collectionId.Value)
            End If
            command.ExecuteNonQuery()
        Loop
    End Sub

    Private Function PickCollection(connection As SqlConnection) As Integer?
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CollectionList
        command.Parameters.AddWithValue(Parameters.NameFilter, Constants.WildCard)
        Dim table As New Dictionary(Of String, Integer)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.PickCollection}
        prompt.AddChoice(MenuItems.NoCollection)
        Using reader = command.ExecuteReader
            While reader.Read
                table($"{reader.GetString(1)}(#{reader.GetInt32(0)})") = reader.GetInt32(0)
            End While
        End Using
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        If answer = MenuItems.NoCollection Then
            Return Nothing
        End If
        Return table(answer)
    End Function

    Private Function PickCategory(connection As SqlConnection) As Integer
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CategoryList
        Dim table As New Dictionary(Of String, Integer)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.PickCategory}
        Using reader = command.ExecuteReader
            While reader.Read
                table(reader.GetString(2)) = reader.GetInt32(0)
            End While
        End Using
        prompt.AddChoices(table.Keys)
        Return table(AnsiConsole.Prompt(prompt))
    End Function

    Private Function PickMediaType(connection As SqlConnection) As Integer
        Dim command = connection.CreateCommand
        command.CommandText = Commands.MediaTypeList
        Dim table As New Dictionary(Of String, Integer)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.PickMediaType}
        Using reader = command.ExecuteReader
            While reader.Read
                table(reader.GetString(2)) = reader.GetInt32(0)
            End While
        End Using
        prompt.AddChoices(table.Keys)
        Return table(AnsiConsole.Prompt(prompt))
    End Function
End Module
