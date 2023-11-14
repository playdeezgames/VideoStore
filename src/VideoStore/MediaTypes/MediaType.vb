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
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.GoBack
                    Exit Do
            End Select
        Loop
    End Sub
End Module
