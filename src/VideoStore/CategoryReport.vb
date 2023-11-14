Imports Microsoft.Data.SqlClient
Imports System.Text

Friend Module CategoryReport
    Friend Sub RunCategoryReport(connection As SqlConnection)
        Dim builder As New StringBuilder
        builder.Append("<html>")
        builder.Append("<head>")
        builder.Append($"<title>Category Report {DateTimeOffset.Now}</title>")
        builder.Append("</head>")
        builder.Append("<body>")
        builder.Append("<table>")
        builder.Append("<tr>")
        builder.Append("<th>Category Name</th>")
        builder.Append("<th>Abbreviation</th>")
        builder.Append("<th>Media Count</th>")
        builder.Append("</tr>")
        Dim command = connection.CreateCommand
        command.CommandText = CategoryReportCommand
        Using reader = command.ExecuteReader
            While reader.Read
                builder.Append("<tr>")
                builder.Append($"<td>{reader.GetString(0)}</td>")
                builder.Append($"<td>{reader.GetString(1)}</td>")
                builder.Append($"<td>{reader.GetInt32(2)}</td>")
                builder.Append("</tr>")
            End While
        End Using
        builder.Append("</table>")
        builder.Append("</body>")
        builder.Append("</html>")
        Dim filename = $"CategoryReport{DateTimeOffset.Now:yyyyMMddHHmmss}.html"
        File.WriteAllText(filename, builder.ToString)
        AnsiConsole.MarkupLine($"Report Saved as {filename}")
        OkPrompt()
    End Sub
End Module
