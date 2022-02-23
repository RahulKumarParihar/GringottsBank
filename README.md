# GringottsBank

Basic Bank API consisting of three endpoint Customer, Account, AccountTranscation

#### Solution contains three projects
1. Bank *(Database solution)*
2. BankLibrary *(Library containing the business logic and data access layer)*
3. GringottsBank *(Enpoints of the project)*

## Dependency
* [NET Core 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* [SQL Server](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)

## Database design
Tables
1. Customer
2. Account
3. CustomerAccount *(Used for mapping customer and account)*
4. AccountTransaction
5. Branch *(Not in use)*


<img width="1768" alt="Bank API" src="https://user-images.githubusercontent.com/31583515/154401038-920feabc-7c13-4bc2-a20e-7f13351faac6.png">

## Setting Up Local Database
* Bank > RightClick > Properties
* Change the Target of Bank according to SQL Server Verson

![image](https://user-images.githubusercontent.com/31583515/155271492-d6ec7094-b587-4e3c-ba1d-bd21e8b8bb2d.png)

* Bank > RightClick > Publish

![image](https://user-images.githubusercontent.com/31583515/155271878-c856555d-6e0f-4b3b-ab80-7b89c678c484.png)

  * Click and edit and set sql server information
* Click on publish
