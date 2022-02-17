CREATE TABLE [dbo].[Customer]
(
	[id] INT NOT NULL IDENTITY(1,1),
	[first_name] VARCHAR(50) NOT NULL, 
    [last_name] VARCHAR(50) NOT NULL, 
    [gender] CHAR NOT NULL, 
    [address] VARCHAR(200) NOT NULL, 
    [dob] DATE NOT NULL,
    CONSTRAINT PK_Customer PRIMARY KEY (id)
)
