Friend Module Commands
    Friend ReadOnly CategoryCheckAbbreviation As String = $"
SELECT 
    COUNT(1) 
FROM 
    {Tables.Categories} 
WHERE 
    {Fields.CategoryAbbr}={Parameters.CategoryAbbr};"

    Friend ReadOnly CategoryDelete As String = $"
DELETE FROM 
    {Tables.Categories} 
WHERE 
    {Fields.CategoryId}={Parameters.CategoryId};"

    Friend ReadOnly CategoryDetails As String = $"
SELECT 
    c.{Fields.CategoryAbbr}, 
    c.{Fields.CategoryName}, 
    COUNT(m.{Fields.MediaId}) {Fields.MediaCount}
FROM 
    {Tables.Categories} c 
    LEFT JOIN {Tables.Media} m ON m.{Fields.CategoryId}=c.{Fields.CategoryId}
GROUP BY
    c.{Fields.CategoryId},
    c.{Fields.CategoryAbbr},
    c.{Fields.CategoryName}
HAVING 
    c.{Fields.CategoryId}={Parameters.CategoryId};"

    Friend ReadOnly CategoryInsert As String = $"
INSERT INTO 
    {Tables.Categories}
    (
        {Fields.CategoryName}, 
        {Fields.CategoryAbbr}
    ) 
    VALUES
    (
        {Parameters.CategoryName}, 
        {Parameters.CategoryAbbr}
    );"

    Friend ReadOnly CategoryList As String = $"
SELECT 
    {Fields.CategoryId}, 
    {Fields.CategoryAbbr}, 
    {Fields.CategoryName} 
FROM 
    {Tables.Categories} 
ORDER BY 
    {Fields.CategoryName};"

    Friend ReadOnly CategoryReport As String = $"
SELECT 
    c.{Fields.CategoryName}, 
    c.{Fields.CategoryAbbr}, 
    c.{Fields.MediaCount} 
FROM 
    {Tables.CategoryListItems} c 
ORDER BY 
    c.{Fields.CategoryName};"

    Friend ReadOnly CategoryUpdateAbbreviation As String = $"
UPDATE 
    {Tables.Categories} 
SET 
    {Fields.CategoryAbbr}={Parameters.CategoryAbbr} 
WHERE 
    {Fields.CategoryId}={Parameters.CategoryId};"

    Friend ReadOnly CategoryUpdateName As String = $"
UPDATE 
    {Tables.Categories} 
SET 
    {Fields.CategoryName}={Parameters.CategoryName} 
WHERE 
    {Fields.CategoryId}={Parameters.CategoryId};"

    Friend ReadOnly CollectionList As String = $"
SELECT 
    c.{Fields.CollectionId}, 
    c.{Fields.CollectionName} 
FROM 
    {Tables.Collections} c 
ORDER BY 
    {Fields.CollectionName};"

    Friend ReadOnly CollectionInsert As String = $"
INSERT INTO 
    {Tables.Collections}
    (
        {Fields.CollectionName}
    ) 
    VALUES
    (
        {Parameters.CollectionName}
    );"

    Friend ReadOnly CollectionDetails As String = $"
SELECT 
    c.{Fields.CollectionId}, 
    c.{Fields.CollectionName}, 
    COUNT(m.MediaId) {Fields.MediaCount}
FROM 
    {Tables.Collections} c 
    LEFT JOIN {Tables.Media} m ON c.{Fields.CollectionId}=m.{Fields.CollectionId} 
GROUP BY 
    c.{Fields.CollectionId}, 
    c.{Fields.CollectionName} 
HAVING 
    c.{Fields.CollectionId}={Parameters.CollectionId};"

    Friend ReadOnly CollectionDelete As String = $"
DELETE FROM 
    {Tables.Collections} 
WHERE 
    {Fields.CollectionId}={Parameters.CollectionId}"

    Friend ReadOnly CollectionUpdateName As String = $"
UPDATE 
    {Tables.Collections} 
SET 
    {Fields.CollectionName}={Parameters.CollectionName} 
WHERE 
    {Fields.CollectionId}={Parameters.CollectionId};"


End Module
