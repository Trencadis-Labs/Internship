USE [master];
GO

CREATE DATABASE [PersonsDB]
ON PRIMARY   
(	
	NAME = PersonsDB,  
    FILENAME = 'D:\Trencadis\Github\Internship\Courses\Apps\Architecture\Intro\NTierBasic\DataAccess\Database\PersonsDB.mdf'
)  
LOG ON  
( 
	NAME = PersonsDB_log,  
    FILENAME = 'D:\Trencadis\Github\Internship\Courses\Apps\Architecture\Intro\NTierBasic\DataAccess\Database\PersonsDB.ldf'
);  
GO  

USE [PersonsDB]
GO

CREATE TABLE [dbo].[Person] (
	[Id]			INT NOT NULL IDENTITY (1, 1),
	[FirstName]		NVARCHAR(100) NULL,
	[LastName]		NVARCHAR(100) NULL,
	[DateOfBirth]	DATETIME NULL,

	Constraint PK_Person PRIMARY KEY(Id));