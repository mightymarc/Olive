﻿ALTER TABLE [Banking].[AccountUser]
    ADD CONSTRAINT [FK_AccountUser_User] FOREIGN KEY ([UserId]) REFERENCES [Auth].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

