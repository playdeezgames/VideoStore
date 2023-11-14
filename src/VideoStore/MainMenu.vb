Imports Microsoft.Data.SqlClient

Friend Module MainMenu
    Friend Sub DoMainMenu(connection As SqlConnection)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MainMenuHeaderText}
            prompt.AddChoice(CategoriesItemText)
            prompt.AddChoice(QuitItemText)
            Select Case AnsiConsole.Prompt(prompt)
                Case CategoriesItemText
                    RunCategories(connection)
                Case QuitItemText
                    Exit Do
            End Select
        Loop
    End Sub
End Module
