ALTER TABLE [Auth].[Session]
	ADD CONSTRAINT [FK_Session_User] 
	FOREIGN KEY (UserId)
	REFERENCES dbo.[User] (UserId);	

