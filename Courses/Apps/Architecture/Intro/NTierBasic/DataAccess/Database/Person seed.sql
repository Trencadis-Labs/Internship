USE [PersonsDB];
GO

DELETE FROM [dbo].[Person];

DBCC CHECKIDENT ('[Person]', RESEED, 0);
GO

DECLARE @Counter INT = 0;
DECLARE @StartDate DATETIME = {d '1970-01-01'};

WHILE @Counter < 100
BEGIN
      
	INSERT INTO [dbo].[Person](
		[FirstName],   [LastName],                                      [DateOfBirth])
	VALUES(
		'Db Generated', 'Person ' + CAST(@Counter + 1 as NVARCHAR(4)),  DateAdd(day, ABS(CHECKSUM(NEWID()) % 15000), @StartDate)
	);

	SET @Counter = @Counter + 1;
END;