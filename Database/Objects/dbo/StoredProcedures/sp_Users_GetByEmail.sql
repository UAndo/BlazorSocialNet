CREATE PROCEDURE [dbo].[sp_Users_GetByEmail]
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE Email = @Email;
END;