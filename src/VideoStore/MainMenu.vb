Imports Microsoft.Data.SqlClient

Friend Module MainMenu
    Friend Sub Run(connection As SqlConnection)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MainMenu}
            prompt.AddChoices(
                MenuItems.Categories,
                MenuItems.Collections,
                MenuItems.MediaTypes,
                MenuItems.Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.Categories
                    Categories.Run(connection)
                Case MenuItems.Collections
                    Collections.Run(connection)
                Case MenuItems.MediaTypes
                    MediaTypes.Run(connection)
                Case MenuItems.Quit
                    Exit Do
            End Select
        Loop
    End Sub
End Module
