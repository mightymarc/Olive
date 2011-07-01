ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [DF_Transfer_CreatedAt] DEFAULT (getutcdate()) FOR [CreatedAt];

