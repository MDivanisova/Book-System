﻿CREATE PROCEDURE GetTop5Books
AS
BEGIN
    SELECT TOP 5 * 
    FROM [dbo].[Books]
    ORDER BY [Sold] DESC;
END