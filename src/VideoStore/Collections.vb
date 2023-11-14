Imports Microsoft.Data.SqlClient

Friend Module Collections
    Friend Sub Run(connection As SqlConnection)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.CollectionsMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewCollection)
            Dim table As New Dictionary(Of String, Integer)
            Dim command = connection.CreateCommand
            command.CommandText = Commands.CollectionList
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
            End Select
        Loop
    End Sub
End Module
