CREATE PROCEDURE [dbo].[sp_Roles_GetIdByName]
	@RoleName nvarchar(64)
AS
	SELECT Id
	FROM Roles
	WHERE Name = @RoleName
RETURN 0
