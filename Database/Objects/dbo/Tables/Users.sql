CREATE TABLE [dbo].[Users]
(
	Id uniqueidentifier PRIMARY KEY NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Username NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    PhomeNumber NVARCHAR(15) NOT NULL,
    Description NVARCHAR(255),
    PasswordHash NVARCHAR(64) NOT NULL,
    Image NVARCHAR(255),
    Language NVARCHAR(10) NOT NULL,
    Location NVARCHAR(255) NULL,
    Role NVARCHAR(64),
    BirthDate DATETIME,
    LastActivityAt DATETIME,
    IsOnline BIT,
    IsLocked BIT NOT NULL DEFAULT 0,
    VerificationToken NVARCHAR(255),
    VerifiedAt DATETIME,
    PasswordResetToken NVARCHAR(255),
    ResetTokenExpires DATETIME
)
