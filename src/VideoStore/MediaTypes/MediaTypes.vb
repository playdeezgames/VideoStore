Imports System.Diagnostics.Eventing
Imports Microsoft.Data.SqlClient
Imports VSData
Friend Module MediaTypes
    Friend Sub Run(store As DataStore)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MenuHeaders.MediaTypesMenu}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.NewMediaType)
            prompt.AddChoice(MenuItems.MediaTypeReport)
            Dim table As New Dictionary(Of String, Integer)
            For Each item In store.MediaTypeList
                Dim mediaTypeId = item.Id
                Dim fullName = $"{item.Name}({item.Abbr})#{mediaTypeId}"
                table(fullName) = mediaTypeId
                prompt.AddChoice(fullName)
            Next
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case MenuItems.NewMediaType
                    NewMediaType.Run(store)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.MediaTypeReport
                    MediaTypeReport.Run(store)
                Case Else
                    MediaType.Run(store, table(answer))
            End Select
        Loop
    End Sub
End Module
