﻿ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [FK_Transfer_FromAccount] FOREIGN KEY ([FromAccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

