Imports Microsoft.Data.SqlClient

Friend Module NewCategory
    Private Const NewCategoryNamePromptText As String = "[olive]New Category Name?[/]"
    Private Const NewCategoryAbbreviationPromptText As String = "[olive]New Category Abbreviation?[/]"
    Private Const CategoryCheckAbbreviationCommandText As String = "SELECT COUNT(1) FROM Categories WHERE CategoryAbbr=@CategoryAbbr;"
    Private Const CategoryAbbrParameterName As String = "@CategoryAbbr"
    Private Const DuplicateAbbreviationErrorText As String = "[red]Duplicate Abbreviation![/]"
    Private Const AddCategoryCommandText As String = "INSERT INTO Categories(CategoryName, CategoryAbbr) VALUES(@CategoryName, @CategoryAbbr);"
    Private Const CategoryNameParameterName As String = "@CategoryName"
    Friend Sub RunNewCategory(connection As SqlConnection)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask(NewCategoryNamePromptText, String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim abbreviation = AnsiConsole.Ask(NewCategoryAbbreviationPromptText, String.Empty)
        If String.IsNullOrWhiteSpace(abbreviation) Then
            Return
        End If
        Dim command = connection.CreateCommand
        command.CommandText = CategoryCheckAbbreviationCommandText
        command.Parameters.AddWithValue(CategoryAbbrParameterName, abbreviation)
        Dim result = CInt(command.ExecuteScalar)
        If result > 0 Then
            AnsiConsole.MarkupLine(DuplicateAbbreviationErrorText)
            OkPrompt()
            Return
        End If
        command = connection.CreateCommand
        command.CommandText = AddCategoryCommandText
        command.Parameters.AddWithValue(CategoryNameParameterName, name)
        command.Parameters.AddWithValue(CategoryAbbrParameterName, abbreviation)
        command.ExecuteNonQuery()
    End Sub
End Module
