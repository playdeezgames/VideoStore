Friend Module Utility
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
        prompt.AddChoice(Ok)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
