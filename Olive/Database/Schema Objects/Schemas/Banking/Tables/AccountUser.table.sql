CREATE TABLE [Banking].[AccountUser] (
    [AccountId]   INT NOT NULL,
    [UserId]      INT NOT NULL,
    [CanDeposit]  BIT NOT NULL,
    [CanWithdraw] BIT NOT NULL
);

