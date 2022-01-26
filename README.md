# Insurance-IS

## About Insurance-IS
[Insurance-IS](https://insurance-is-dev.azurewebsites.net/) is an information system for insurance agents and their supervisors. The supervisors can create types and subtypes of insurances. Also they can create object types that define what kind of objects a client can insure. They can also see statistics of how much money an insurance agent made in a specific insurance type and which insurance subtypes are most common.

Insurance agents can create a client, create an object that belongs to specific client and create an insurance policy for specific client and specific object.

## Download and Installation
* Clone the repository: `git clone https://github.com/lenartgolob/Insurance-IS.git`
* Host MS SQL server: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU13-ubuntu-20.04`
* Update database: `dotnet ef database update`
* Run the project: `dotnet run`

## Preview
**Landing Page** is accesible to all users and from here a user can login/register or access other functionalities of the website.

![Landing Page](https://user-images.githubusercontent.com/38143019/148383071-24f42adf-155d-44fd-99d0-26ea694326da.png)

**Insure a client** is a page designed for agents to make the insurance process as seamless as possible. This page is restricted, it's open only for agents and their supervisors. 
![Insure a client](https://user-images.githubusercontent.com/38143019/148383243-cc4b3043-7168-4a77-afbb-bafededca58b.png)

**Agent panel** is a page where and agent can see all of his available functionalities. CRUD operations are implemented for every available functionality. This page is restricted, it's open only for agents and their supervisors.
![Agent panel](https://user-images.githubusercontent.com/38143019/148383919-76efd993-3cf8-456f-a398-eea45dccf92b.png)

**Admin panel** is a page where a supervisor can see all of his avaible functionalities and some statistics about agents and made insurances. This page is restricted, it's open only for supervisors.
![Admin panel](https://user-images.githubusercontent.com/38143019/148384602-0acf67e5-cde1-4d20-8944-317be8bcedca.png)

**Insurance Policy Details** page displays all the final information about an insurance policy, it's also possible to generate a PDF file of a insurance policy. This page is restricted, it's open only for agents and their supervisors.
![Insurance policy](https://user-images.githubusercontent.com/38143019/148384951-e6c0daa3-c7af-4a0c-ac59-fd61dcda86b1.png)

## Database
### Structure of Database
Database consists of 6 tables. Table Insured is a table for clients and table InsuranceSubject is a table for an object that is / will be insured. Other tables are pretty self-explanatory.
![Database Scheme](https://user-images.githubusercontent.com/75984427/148302654-84f8028b-d59d-4351-b285-0e8dd918803c.png)

### Authentication and authorization
Library AspNetCore.Identity is implemented for authentication and authorization purposes.
![image](https://user-images.githubusercontent.com/38143019/148382626-6b79a92e-9bca-4a9c-9b11-55dc2b3fe692.png)

## REST API
API is documented [here](https://insurance-is-dev.azurewebsites.net/swagger/index.html). The API allows read, create, update and delete operations on tables InsurancePolicy, Insured and InsuranceSubject.
