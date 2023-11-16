Friend Module Media
    Friend Sub Run(store As DataStore)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MediaMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewMedia)
            prompt.AddChoice(MenuItems.MediaReport)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.NewMedia
                    NewMedia.Run(store)
                Case MenuItems.MediaReport
                    MediaReport.Run(store)
            End Select
        Loop
    End Sub
End Module
