CREATE TABLE [dbo].[Roles]
(
	Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(64) NOT NULL
)
