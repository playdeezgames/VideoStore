Imports Microsoft.Data.SqlClient

Friend Module MediaType
    Friend Sub Run(connection As SqlConnection, mediaTypeId As Integer)
        Do
            Dim command = connection.CreateCommand
            command.CommandText = Commands.MediaTypeDetails
            command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
            Dim mediaTypeName As String
            Dim mediaTypeAbbr As String
            Dim mediaCount As Integer
            Using reader = command.ExecuteReader
                reader.Read()
                mediaTypeName = reader.GetString(2)
                mediaTypeAbbr = reader.GetString(1)
                mediaCount = reader.GetInt32(3)
            End Using
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {mediaTypeId}")
            AnsiConsole.MarkupLine($"Name: {mediaTypeName}")
            AnsiConsole.MarkupLine($"Abbr: {mediaTypeAbbr}")
            AnsiConsole.MarkupLine($"Media Count: {mediaCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.ChangeName)
            prompt.AddChoice(MenuItems.ChangeAbbreviation)
            If mediaCount = 0 Then
                prompt.AddChoice(MenuItems.DeleteMediaType)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.DeleteMediaType
                    DeleteMediaType.Run(connection, mediaTypeId)
                    Exit Do
                Case MenuItems.ChangeAbbreviation
                    ChangeMediaTypeAbbreviation(connection, mediaTypeId, mediaTypeAbbr)
                Case MenuItems.ChangeName
                    ChangeMediaTypeName(connection, mediaTypeId, mediaTypeName)
            End Select
        Loop
    End Sub

    Private Sub ChangeMediaTypeName(
                                   connection As SqlConnection,
                                   mediaTypeId As Integer,
                                   mediaTypeName As String)
        Dim newMediaTypeName = AnsiConsole.Ask(Prompts.NewMediaTypeName, mediaTypeName)
        If newMediaTypeName = mediaTypeName Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = Commands.MediaTypeUpdateName
        command.Parameters.AddWithValue(Parameters.MediaTypeName, newMediaTypeName)
        command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
        command.ExecuteNonQuery()
    End Sub

    Private Sub ChangeMediaTypeAbbreviation(
                                           connection As SqlConnection,
                                           mediaTypeId As Integer,
                                           mediaTypeAbbr As String)
        Dim newMediaTypeAbbr = AnsiConsole.Ask(Prompts.NewMediaTypeName, mediaTypeAbbr)
        If newMediaTypeAbbr = mediaTypeAbbr Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = Commands.MediaTypeCheckAbbreviation
        command.Parameters.AddWithValue(Parameters.MediaTypeAbbr, newMediaTypeAbbr)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(Errors.DuplicateAbbreviation)
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = Commands.MediaTypeUpdateAbbr
        command.Parameters.AddWithValue(Parameters.MediaTypeAbbr, newMediaTypeAbbr)
        command.Parameters.AddWithValue(Parameters.MediaTypeId, mediaTypeId)
        command.ExecuteNonQuery()
    End Sub
End Module
