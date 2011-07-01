ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [CK_Transfer_Amount] CHECK ([Amount]>(0));

