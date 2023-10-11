CREATE PROCEDURE [dbo].[sp_Users_GetByToken]
	@Token NVARCHAR(255)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE VerificationToken = @Token;
END;
