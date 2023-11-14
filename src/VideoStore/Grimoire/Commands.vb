Friend Module Commands
    Friend ReadOnly CategoryCheckAbbreviation As String = $"SELECT COUNT(1) FROM Categories WHERE CategoryAbbr={CategoryAbbrParameterName};"
    Friend ReadOnly CategoryDelete As String = $"DELETE FROM Categories WHERE CategoryId={CategoryIdParameterName};"
    Friend ReadOnly CategoryDetails As String = $"
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
    Friend ReadOnly CategoryInsert As String = $"INSERT INTO Categories(CategoryName, CategoryAbbr) VALUES({CategoryNameParameterName}, {CategoryAbbrParameterName});"
    Friend Const CategoryList As String = "SELECT CategoryId, CategoryAbbr, CategoryName FROM Categories ORDER BY CategoryName;"
    Friend Const CategoryReport As String = "SELECT c.CategoryName, c.CategoryAbbr, c.MediaCount FROM CategoryListItems c ORDER BY c.CategoryName;"
    Friend ReadOnly CategoryUpdateAbbreviation As String = $"UPDATE Categories SET CategoryAbbr={CategoryAbbrParameterName} WHERE CategoryId={CategoryIdParameterName};"
    Friend ReadOnly CategoryUpdateName As String = $"UPDATE Categories SET CategoryName={CategoryNameParameterName} WHERE CategoryId={CategoryIdParameterName};"
End Module
