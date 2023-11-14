Friend Module Commands
    Friend ReadOnly CategoryCheckAbbreviationCommandText As String = $"SELECT COUNT(1) FROM Categories WHERE CategoryAbbr={CategoryAbbrParameterName};"
    Friend ReadOnly CategoryDelete As String = $"DELETE FROM Categories WHERE CategoryId={CategoryIdParameterName};"
    Friend ReadOnly CategoryDetailsCommandText As String = $"
SELECT 
    c.CategoryAbbr, 
    c.CategoryName, 
    COUNT(m.MediaId) MediaCount
FROM 
    Categories c 
    LEFT JOIN Media m ON m.CategoryId=c.CategoryId
GROUP BY
    c.CategoryId,
    c.CategoryAbbr,
    c.CategoryName
HAVING 
    c.CategoryId={CategoryIdParameterName};"
    Friend ReadOnly CategoryInsertCommandText As String = $"INSERT INTO Categories(CategoryName, CategoryAbbr) VALUES({CategoryNameParameterName}, {CategoryAbbrParameterName});"
    Friend Const CategoryListCommandText As String = "SELECT CategoryId, CategoryAbbr, CategoryName FROM Categories ORDER BY CategoryName;"
    Friend Const CategoryReportCommand As String = "SELECT c.CategoryName, c.CategoryAbbr, c.MediaCount FROM CategoryListItems c ORDER BY c.CategoryName;"
    Friend ReadOnly CategoryUpdateAbbreviation As String = $"UPDATE Categories SET CategoryAbbr={CategoryAbbrParameterName} WHERE CategoryId={CategoryIdParameterName};"
    Friend ReadOnly CategoryUpdateName As String = $"UPDATE Categories SET CategoryName={CategoryNameParameterName} WHERE CategoryId={CategoryIdParameterName};"
End Module
