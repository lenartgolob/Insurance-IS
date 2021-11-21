# Insurance-IS

## About Insurance-IS
The insurance information system enables the addition of policyholders, types of insurance and the creation of insurance policies. The IS offers the option to register and log in, unregistered user will only have access to the landing page. IS is primarily intended for insurance agents who approach the client directly for whom they open a insurance policy, where the client also sees the price of the insurance policy before the agent confirms it. The agent also has an overview of all insured people via IS, where he can also edit and delete their details.


## Download and Installation

* Clone the repository: `git clone https://github.com/lenartgolob/Insurance-IS.git`
* Host MS SQL server: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU13-ubuntu-20.04`
* Update database: `dotnet ef database update`
* Run the project: `dotnet run`

## Authors
* Lenart Golob 63200101
* Anže Novak 63200202 

