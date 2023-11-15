﻿Friend Module Commands
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

    Friend ReadOnly MediaTypeCheckAbbreviation As String = $"
SELECT
    COUNT(1)
FROM
    {Tables.MediaTypes}
WHERE
    {Fields.MediaTypeAbbr}={Parameters.MediaTypeAbbr};"

    Friend ReadOnly MediaTypeInsert As String = $"
INSERT INTO
    {Tables.MediaTypes}
    (
        {Fields.MediaTypeAbbr},
        {Fields.MediaTypeName}
    )
    VALUES
    (
        {Parameters.MediaTypeAbbr},
        {Parameters.MediaTypeName}
    );"

    Friend ReadOnly MediaTypeList As String = $"
SELECT
    {Fields.MediaTypeId},
    {Fields.MediaTypeName},
    {Fields.MediaTypeAbbr}
FROM
    {Tables.MediaTypes}
ORDER BY
    {Fields.MediaTypeName};"

    Friend ReadOnly MediaTypeDetails As String = $"
SELECT
    mt.{Fields.MediaTypeId},
    mt.{Fields.MediaTypeAbbr},
    mt.{Fields.MediaTypeName},
    COUNT(m.{Fields.MediaId}) AS {Fields.MediaCount}
FROM
    {Tables.MediaTypes} mt
    LEFT JOIN {Tables.Media} m ON m.{Fields.MediaTypeId}=mt.{Fields.MediaTypeId}
GROUP BY
    mt.{Fields.MediaTypeId},
    mt.{Fields.MediaTypeAbbr},
    mt.{Fields.MediaTypeName}
HAVING
    mt.{Fields.MediaTypeId}={Parameters.MediaTypeId};"

    Friend ReadOnly CollectionReport As String = $"
SELECT
    c.{Fields.CollectionName},
    COUNT(m.{Fields.MediaId}) AS {Fields.MediaCount}
FROM
    {Tables.Collections} c
    LEFT JOIN {Tables.Media} m ON m.{Fields.CollectionId}=c.{Fields.CollectionId}
GROUP BY
    c.{Fields.CollectionId},
    c.{Fields.CollectionName}
ORDER BY
    c.{Fields.CollectionName};"

    Friend ReadOnly MediaTypeDelete As String = $"
DELETE FROM 
    {Tables.MediaTypes}
WHERE
    {Fields.MediaTypeId}={Parameters.MediaTypeId};"

    Friend ReadOnly MediaTypeReport As String = $"
SELECT
    mt.{Fields.MediaTypeName},
    mt.{Fields.MediaTypeAbbr},
    COUNT(m.{Fields.MediaId}) AS {Fields.MediaCount}
FROM
    {Tables.MediaTypes} mt
    LEFT JOIN {Tables.Media} m ON m.{Fields.MediaTypeId}=mt.{Fields.MediaTypeId}
GROUP BY
    mt.{Fields.MediaTypeId},
    mt.{Fields.MediaTypeAbbr},
    mt.{Fields.MediaTypeName}
ORDER BY
    mt.{Fields.MediaTypeName};"

    Friend ReadOnly MediaTypeUpdateName As String = $"
UPDATE
    {Tables.MediaTypes}
SET
    {Fields.MediaTypeName}={Parameters.MediaTypeName}
WHERE
    {Fields.MediaTypeId}={Parameters.MediaTypeId};"

    Friend ReadOnly MediaTypeUpdateAbbr As String = $"
UPDATE
    {Tables.MediaTypes}
SET
    {Fields.MediaTypeAbbr}={Parameters.MediaTypeAbbr}
WHERE
    {Fields.MediaTypeId}={Parameters.MediaTypeId};"

End Module
