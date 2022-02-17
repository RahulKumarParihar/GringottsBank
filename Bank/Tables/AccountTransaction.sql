CREATE TABLE [dbo].[AccountTransaction]
(
	[id] INT NOT NULL, 
    [amount] DECIMAL NOT NULL, 
    [entry_date] DATETIME NOT NULL DEFAULT GetDate(), 
    [account_id] INT NOT NULL, 
    [type] CHAR(1) NOT NULL, 
    CONSTRAINT PK_AccountTransaction PRIMARY KEY (id),
    CONSTRAINT FK_AccountTransaction_Account FOREIGN KEY ([account_id])
    REFERENCES Account(id)
)
