Imports Microsoft.Data.SqlClient
Friend Module MediaTypes
    Friend Sub Run(connection As SqlConnection)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MediaTypesMenu}
            prompt.AddChoice(MenuItems.GoBack)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case MenuItems.GoBack
                    Exit Do
            End Select
        Loop
    End Sub
End Module
