CREATE PROCEDURE [dbo].[sp_UserRoles_AddToRole]
	@UserId uniqueidentifier,
	@RoleId uniqueidentifier
AS
	INSERT UserRoles (UserId, RoleId)
	VALUES (@UserId,@RoleId)

RETURN 0
