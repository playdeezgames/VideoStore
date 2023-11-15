Imports System.Windows.Markup
Imports Microsoft.Data.SqlClient

Friend Module Collections
    Friend Sub Run(connection As SqlConnection)
        Dim nameFilter As String = Constants.WildCard
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Name Filter: {nameFilter}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.CollectionsMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewCollection)
            prompt.AddChoice(MenuItems.ChangeNameFilter)
            prompt.AddChoice(MenuItems.CollectionReport)
            Dim table As New Dictionary(Of String, Integer)
            Dim command = connection.CreateCommand
            command.CommandText = Commands.CollectionList
            command.Parameters.AddWithValue(Parameters.NameFilter, nameFilter)
            Using reader = command.ExecuteReader()
                While reader.Read
                    Dim fullname = $"{reader.GetString(1)}(#{reader.GetInt32(0)})"
                    table(fullname) = reader.GetInt32(0)
                    prompt.AddChoice(fullname)
                End While
            End Using
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.NewCollection
                    NewCollection.Run(connection)
                Case MenuItems.CollectionReport
                    CollectionReport.Run(connection)
                Case MenuItems.ChangeNameFilter
                    nameFilter = AnsiConsole.Ask(Prompts.NewNameFilter, Constants.WildCard)
                Case Else
                    CollectionItem.Run(connection, table(answer))
            End Select
        Loop
    End Sub
End Module
