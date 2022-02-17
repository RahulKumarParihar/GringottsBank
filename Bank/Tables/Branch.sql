CREATE TABLE [dbo].[Branch]
(
	[id] INT NOT NULL,
	[name] varchar(100) NOT NULL, 
    [code] VARCHAR(50) NOT NULL, 
    [address] VARCHAR(200) NOT NULL,
	CONSTRAINT PK_Branch PRIMARY KEY (id),
	CONSTRAINT UK_Branch UNIQUE (code),
)
