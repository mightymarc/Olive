ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [CK_Transfer_Volume] CHECK ([Volume]>(0));

