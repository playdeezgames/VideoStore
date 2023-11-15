Imports System.Text
Imports Microsoft.Data.SqlClient

Friend Module Utility
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
        prompt.AddChoice(Ok)
        AnsiConsole.Prompt(prompt)
    End Sub
    Friend Sub ExportHtml(
                         reportTitle As String,
                         filenamePrefix As String,
                         columnHeaders As String(),
                         command As SqlCommand,
                         columnSources As Func(Of SqlDataReader, Object)())
        Dim builder As New StringBuilder
        Dim reportTime = DateTimeOffset.Now
        builder.Append("<html>")
        builder.Append("<head>")
        builder.Append($"<title>{reportTitle} {reportTime}</title>")
        builder.Append("</head>")
        builder.Append("<body>")
        builder.Append("<table>")
        builder.Append("<tr>")
        For Each columnHeader In columnHeaders
            builder.Append($"<th>{columnHeader}</th>")
        Next
        builder.Append("</tr>")
        Using reader = command.ExecuteReader
            While reader.Read
                builder.Append("<tr>")
                For Each columnSource In columnSources
                    builder.Append($"<td>{columnSource(reader)}</td>")
                Next
                builder.Append("</tr>")
            End While
        End Using
        builder.Append("</table>")
        builder.Append("</body>")
        builder.Append("</html>")
        Dim filename = $"{filenamePrefix}{reportTime:yyyyMMddHHmmss}.html"
        File.WriteAllText(filename, builder.ToString)
        AnsiConsole.MarkupLine($"Report Saved as {filename}")
        OkPrompt()

    End Sub
End Module
