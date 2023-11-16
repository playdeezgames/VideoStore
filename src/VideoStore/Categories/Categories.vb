Imports VSData

Friend Module Categories
    Friend Sub Run(store As DataStore)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = CategoriesMenu}
            prompt.AddChoice(GoBack)
            prompt.AddChoice(MenuItems.NewCategory)
            prompt.AddChoice(MenuItems.CategoryReport)
            Dim table As New Dictionary(Of String, Integer)
            Dim items As IEnumerable(Of (Id As Integer, Abbr As String, Name As String)) = store.CategoryList
            For Each item In items
                Dim fullName = $"{item.Name}({item.Abbr})#{item.Id}"
                prompt.AddChoice(fullName)
                table(fullName) = item.Id
            Next
            AnsiConsole.Clear()
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBack
                    Exit Do
                Case MenuItems.NewCategory
                    NewCategory.Run(store)
                Case MenuItems.CategoryReport
                    CategoryReport.Run(store)
                Case Else
                    Category.Run(store, table(answer))
            End Select
        Loop
    End Sub
End Module
