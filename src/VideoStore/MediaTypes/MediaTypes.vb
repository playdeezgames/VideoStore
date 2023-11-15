﻿Imports Microsoft.Data.SqlClient
Friend Module MediaTypes
    Friend Sub Run(connection As SqlConnection)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MediaTypesMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewMediaType)
            prompt.AddChoice(MenuItems.MediaTypeReport)
            Dim command = connection.CreateCommand
            command.CommandText = Commands.MediaTypeList
            Dim table As New Dictionary(Of String, Integer)
            Using reader = command.ExecuteReader
                While reader.Read
                    Dim mediaTypeId = reader.GetInt32(0)
                    Dim fullName = $"{reader.GetString(1)}({reader.GetString(2)})#{mediaTypeId}"
                    table(fullName) = mediaTypeId
                    prompt.AddChoice(fullName)
                End While
            End Using
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case MenuItems.NewMediaType
                    NewMediaType.Run(connection)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.MediaTypeReport
                    MediaTypeReport.Run(connection)
                Case Else
                    MediaType.Run(connection, table(answer))
            End Select
        Loop
    End Sub
End Module