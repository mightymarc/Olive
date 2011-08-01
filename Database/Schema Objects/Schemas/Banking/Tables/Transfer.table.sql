CREATE TABLE [Banking].[Transfer] (
    [TransferId]      BIGINT          identity NOT NULL,
    [FromAccountId] INT             NOT NULL,
    [ToAccountId]   INT             NOT NULL,
    [Volume]          DECIMAL (18, 8) NOT NULL,
    [CreatedAt]       DATETIME        NOT NULL,
    [FromComment] NVARCHAR (250) NOT NULL,
    [ToComment] NVARCHAR (250) NOT NULL,
);

