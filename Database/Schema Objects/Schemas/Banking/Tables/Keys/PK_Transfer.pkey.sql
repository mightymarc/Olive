﻿ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED ([TransferId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

