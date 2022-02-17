CREATE TABLE [dbo].[Account]
(
	[id] INT NOT NULL, 
    [branch_id] INT NOT NULL, 
    [balance] DECIMAL NOT NULL, 
    [create_date] DATETIME NOT NULL DEFAULT GetDate(), 
    [close_date] DATETIME NULL,
    CONSTRAINT PK_Account PRIMARY KEY (id),
    CONSTRAINT FK_Account_Branch FOREIGN KEY (branch_id)
    REFERENCES Branch(id)
)
