Friend Module Commands
    Friend Const CategoryListCommandText As String = "SELECT CategoryId, CategoryAbbr, CategoryName FROM Categories ORDER BY CategoryName;"
    Friend Const CategoryDetailCommandText As String = "
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
    c.CategoryId=@CategoryId;"
    Friend Const UpdateCategoryAbbreviation As String = "UPDATE Categories SET CategoryAbbr=@CategoryAbbr WHERE CategoryId=@CategoryId;"
    Friend Const UpdateCategoryName As String = "UPDATE Categories SET CategoryName=@CategoryName WHERE CategoryId=@CategoryId;"
    Friend Const CategoryReportCommand As String = "SELECT c.CategoryName, c.CategoryAbbr, c.MediaCount FROM CategoryListItems c ORDER BY c.CategoryName;"
    Friend Const CategoryDelete As String = "DELETE FROM Categories WHERE CategoryId=@CategoryId;"
End Module
