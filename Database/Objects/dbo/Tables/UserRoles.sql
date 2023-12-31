﻿CREATE TABLE [dbo].[UserRoles]
(
	[UserId] UNIQUEIDENTIFIER,
	[RoleId] UNIQUEIDENTIFIER,
	PRIMARY KEY (UserId, RoleId),
	FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
)
