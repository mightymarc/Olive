CREATE TABLE [Banking].[Account] (
    [AccountId]   INT            IDENTITY (1000, 1) NOT NULL,
    [DisplayName] NVARCHAR (150),
    [CurrencyId]  VARCHAR(10)            NOT NULL,
    [Type]        VARCHAR (50)   NOT NULL,
	[AllowNegative] BIT NOT NULL default(0),
	[AnyCanDeposit] BIT NOT NULL default(0)
);