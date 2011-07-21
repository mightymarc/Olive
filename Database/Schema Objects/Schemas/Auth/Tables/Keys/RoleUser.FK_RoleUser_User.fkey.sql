ALTER TABLE [Auth].[RoleUser]
	ADD CONSTRAINT [FK_RoleUser_User] 
	FOREIGN KEY (UserId)
	REFERENCES [Auth].[User] (UserId);	

