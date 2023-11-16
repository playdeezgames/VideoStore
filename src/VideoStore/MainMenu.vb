Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module MainMenu
    Friend Sub Run(store As DataStore)
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MainMenu}
            prompt.AddChoices(
                MenuItems.Media,
                MenuItems.Collections,
                MenuItems.Categories,
                MenuItems.MediaTypes,
                MenuItems.Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.Media
                    Media.Run(store)
                Case MenuItems.Categories
                    Categories.Run(store)
                Case MenuItems.Collections
                    Collections.Run(store)
                Case MenuItems.MediaTypes
                    MediaTypes.Run(store)
                Case MenuItems.Quit
                    Exit Do
            End Select
        Loop
    End Sub
End Module
