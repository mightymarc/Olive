ALTER TABLE [Auth].[User]
	ADD CONSTRAINT [FK_User_ParentUser] 
	FOREIGN KEY (UserId)
	REFERENCES Auth.[User] (UserId);	
