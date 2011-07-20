ALTER TABLE [Auth].[RoleUser]
	ADD CONSTRAINT [FK_RoleUser_Role] 
	FOREIGN KEY (RoleId)
	REFERENCES Auth.[Role] (RoleId);	

