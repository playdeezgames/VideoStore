Imports Microsoft.Data.SqlClient

Friend Module Categories
    Private Const GoBackItemText As String = "Go Back"
    Private Const CategoriesMenuHeaderText As String = "[olive]Categories Menu:[/]"
    Private Const NewCategoryItemText As String = "New Category..."
    Private Const CategoryListCommandText As String = "SELECT CategoryId, CategoryAbbr, CategoryName FROM Categories ORDER BY CategoryName;"
    Private Const CategoryReportItemText = "Category Report"
    Friend Sub RunCategories(connection As SqlConnection)
        Do
            Dim categoryList As New List(Of (Id As Integer, Abbr As String, Name As String))
            Dim prompt As New SelectionPrompt(Of String) With {.Title = CategoriesMenuHeaderText}
            prompt.AddChoice(GoBackItemText)
            prompt.AddChoice(NewCategoryItemText)
            prompt.AddChoice(CategoryReportItemText)
            Dim table As New Dictionary(Of String, Integer)
            Using command = connection.CreateCommand
                command.CommandText = CategoryListCommandText
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
