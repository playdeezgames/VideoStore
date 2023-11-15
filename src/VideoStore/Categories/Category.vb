﻿Imports Microsoft.Data.SqlClient

Friend Module Category
    Friend Sub Run(connection As SqlConnection, categoryId As Integer)
        Do
            Dim category As (Id As Integer, Abbr As String, Name As String, MediaCount As Integer)
            Dim command = connection.CreateCommand()
            command.CommandText = CategoryDetails
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            Using reader = command.ExecuteReader()
                reader.Read()
                category = (categoryId, reader.GetString(0), reader.GetString(1), reader.GetInt32(2))
            End Using
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Id: {category.Id}")
            AnsiConsole.MarkupLine($"Abbreviation: {category.Abbr}")
            AnsiConsole.MarkupLine($"Name: {category.Name}")
            AnsiConsole.MarkupLine($"Media: {category.MediaCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = NowWhat}
            prompt.AddChoice(GoBack)
            prompt.AddChoice(ChangeName)
            prompt.AddChoice(ChangeAbbreviation)
            If category.MediaCount = 0 Then
                prompt.AddChoice(MenuItems.DeleteCategory)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBack
                    Exit Do
                Case MenuItems.DeleteCategory
                    DeleteCategory.Run(connection, categoryId)
                    Exit Do
                Case ChangeName
                    ChangeCategoryName(connection, categoryId, category.Name)
                Case ChangeAbbreviation
                    ChangeCategoryAbbreviation(connection, categoryId, category.Abbr)
            End Select
        Loop
    End Sub


    Private Sub ChangeCategoryAbbreviation(connection As SqlConnection, categoryId As Integer, abbr As String)
        Dim newAbbreviation = AnsiConsole.Ask("[olive]New Abbreviation?[/]", abbr)
        If newAbbreviation <> abbr Then
            Dim command = connection.CreateCommand
            command.CommandText = CategoryUpdateAbbreviation
            command.Parameters.AddWithValue(Parameters.CategoryAbbr, newAbbreviation)
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
    Private Sub ChangeCategoryName(connection As SqlConnection, categoryId As Integer, name As String)
        Dim newName = AnsiConsole.Ask("[olive]New Name?[/]", name)
        If newName <> name Then
            Dim command = connection.CreateCommand
            command.CommandText = CategoryUpdateName
            command.Parameters.AddWithValue(Parameters.CategoryName, newName)
            command.Parameters.AddWithValue(Parameters.CategoryId, categoryId)
            command.ExecuteNonQuery()
        End If
    End Sub
End Module