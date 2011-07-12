CREATE TABLE [Banking].[Transfer] (
    [TransferId]      BIGINT          identity NOT NULL,
    [SourceAccountId] INT             NOT NULL,
    [DestAccountId]   INT             NOT NULL,
    [Amount]          DECIMAL (18, 8) NOT NULL,
    [CreatedAt]       DATETIME        NOT NULL,
    [Description]     NVARCHAR (250)  NOT NULL
);

