﻿ALTER TABLE [Banking].[Account]
    ADD CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

