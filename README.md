# Shopping Website

Implemented with C# Window Form Application, ASP .NET Framework. Using Visual Studio 2022 for implementation.

## Description

Window Form Application for Pi Store Management. Basically can manage its employee, client, product, place orders and print bill. Also can view income analytics of last 7 days, by day, week, month, year.

This application is only used by Administrator who has login account technically fully access of functional. We can develop this application for employees but need more authorization. 

On the Dashboard view, we can view the number of employees, clients, products, orders and bills which is existed within our system (can dynamically changes by using basic operation of each management).

Employee Management

* Add, Update, Delete, Search filter and Export CSV of current display employee list.

Client, Product Management is as same functional as Employee management.

Order Management

* Add, Update, Delete, Search filter and Export CSV of order
* Add, Update, Delete order item

This order item exist when and only when the order is existed. Therefore, to add order item, we indeed to create a order first, then we can perform operations on order item.

Bill Management

* Search filter and Export PDF for bill

Each of bill in list including order items in the order. We can not delete bill but we can delete order to delete bill. This is the missing of this system, which very difficult for authorize for employees to use this system.
Which can makes of a cheating and misunderstanding of each other.

Analytisc

* View the line chart, column chart and pie chart of total income analytics by time period
  * Last 7 days
  * Day - day by day total income
  * Week - day within week total income 
  * Month - day within month total income
  * Year - month within year total income

## Requirements

* Visual Studio 2022
* ASP .NET Framework (version 4.8)

## Usage

To run the system, please clone the repository first with

` git clone https://github.com/giahao1411/pi-store-management-system.git `

After that, open Visual Studio 2022, select ` Open a project or solution `

Choose ` PiStoreManagement.sln ` on your download Folder

Open project and choose ` Start ` on the Tool Bar or you can choose ` Debug ` then ` Start debugging ` to run the project.

## License

This project is licensed under the [GNU GPLv3 License](./LICENSE).

## Author

This program was created by 

* [giahao1411](https://github.com/giahao1411)


Thanks for visting my project!
