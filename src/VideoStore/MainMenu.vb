Imports Microsoft.Data.SqlClient

Friend Module MainMenu
    Friend Sub Run(connection As SqlConnection)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MainMenu}
            prompt.AddChoice(MenuItems.Categories)
            prompt.AddChoice(MenuItems.Collections)
            prompt.AddChoice(Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.Categories
                    Categories.Run(connection)
                Case MenuItems.Collections
                    Collections.Run(connection)
                Case Quit
                    Exit Do
            End Select
        Loop
    End Sub
End Module
