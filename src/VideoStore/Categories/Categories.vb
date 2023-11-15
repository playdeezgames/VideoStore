Imports Microsoft.Data.SqlClient

Friend Module Categories
    Friend Sub Run(store As DataStore)
        Do
            Dim categoryList As New List(Of (Id As Integer, Abbr As String, Name As String))
            Dim prompt As New SelectionPrompt(Of String) With {.Title = CategoriesMenu}
            prompt.AddChoice(GoBack)
            prompt.AddChoice(MenuItems.NewCategory)
            prompt.AddChoice(MenuItems.CategoryReport)
            Dim table As New Dictionary(Of String, Integer)
            Using command = store.Connection.CreateCommand
                command.CommandText = Commands.CategoryList
                Using reader = command.ExecuteReader
                    While reader.Read
                        Dim item As (Id As Integer, Abbr As String, Name As String) = (reader.GetInt32(0), reader.GetString(1), reader.GetString(2))
                        categoryList.Add(item)
                        Dim fullName = $"{item.Name}({item.Abbr})#{item.Id}"
                        prompt.AddChoice(fullName)
                        table(fullName) = item.Id
                    End While
                End Using
            End Using
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
