﻿CREATE TABLE [Banking].[Account] (
    [AccountId]   INT            IDENTITY (1000, 1) NOT NULL,
    [DisplayName] NVARCHAR (150) NOT NULL default(''),
    [CurrencyId]  INT            NOT NULL,
    [Type]        VARCHAR (50)   NOT NULL,
	[AllowNegative] bit not null default(0)
);