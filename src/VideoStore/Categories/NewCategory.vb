Imports VSData

Friend Module NewCategory
    Friend Sub Run(store As DataStore)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryName, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviation, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        If store.CheckCategoryAbbreviation(abbreviation) Then
            AnsiConsole.MarkupLine(DuplicateAbbreviation)
            OkPrompt()
            Return
        End If
        store.CreateCategory(name, abbreviation)
    End Sub
End Module
