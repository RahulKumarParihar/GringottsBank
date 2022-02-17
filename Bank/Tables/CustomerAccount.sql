CREATE TABLE [dbo].[CustomerAccount]
(
	[account_id] INT NOT NULL,
	[customer_id] INT NOT NULL,
	CONSTRAINT FK_CustomerAccount_Account FOREIGN KEY ([account_id])
    REFERENCES Account(id),
	CONSTRAINT FK_CustomerAccount_Customer FOREIGN KEY ([customer_id])
    REFERENCES Customer(id)
)
