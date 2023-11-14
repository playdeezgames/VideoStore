Imports Microsoft.Data.SqlClient

Friend Module Categories
    Friend Sub RunCategories(connection As SqlConnection)
        Do
            Dim categoryList As New List(Of (Id As Integer, Abbr As String, Name As String))
            Dim prompt As New SelectionPrompt(Of String) With {.Title = CategoriesMenu}
            prompt.AddChoice(GoBackItemText)
            prompt.AddChoice(NewCategoryItemText)
            prompt.AddChoice(CategoryReportItemText)
            Dim table As New Dictionary(Of String, Integer)
            Using command = connection.CreateCommand
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
                Case GoBackItemText
                    Exit Do
                Case NewCategoryItemText
                    RunNewCategory(connection)
                Case CategoryReportItemText
                    RunCategoryReport(connection)
                Case Else
                    RunCategory(connection, table(answer))
            End Select
        Loop
    End Sub
End Module
