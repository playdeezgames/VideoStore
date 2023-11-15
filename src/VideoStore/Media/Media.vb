Imports Microsoft.Data.SqlClient

Friend Module Media
    Friend Sub Run(connection As SqlConnection)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MediaMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewMedia)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.NewMedia
                    NewMedia.Run(connection)
            End Select
        Loop
    End Sub
End Module
