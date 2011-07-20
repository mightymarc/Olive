ALTER TABLE [Banking].[Account]
    ADD CONSTRAINT [CK_Account_Type] CHECK ([Type]='Current' OR [Type]='Special');

