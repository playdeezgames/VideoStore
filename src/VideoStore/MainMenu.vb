Imports Microsoft.Data.SqlClient

Friend Module MainMenu
    Friend Sub DoMainMenu(connection As SqlConnection)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MainMenu}
            prompt.AddChoice(MenuItems.Categories)
            prompt.AddChoice(Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.Categories
                    RunCategories(connection)
                Case Quit
                    Exit Do
            End Select
        Loop
    End Sub
End Module
