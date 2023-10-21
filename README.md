<h1 align="center">
  Book Shop System
</h1>

<p align="center">
  Welcome to Book Shop System, the ultimate platform for Buying and Selling Books.
</p>


# Introduction
"Book Shop System" is a web application built using ASP.NET Core that provides a convenient and user-friendly platform for buyers and sellers to interact in the process of buying and selling books. Whether you're looking to purchase a new book or wanting to sell your old one, "Book Shop System" has you covered.


## :ledger: Index
- [About](#beginner-about)
- [Installation](#electric_plug-installation)
- [Credentials](#key-credentials)
- [Build With](#hammer-build-with)
- [Gallery](#camera-gallery)



##  :beginner: About
"Book Shop System" caters to four user types: three registered and one unregistered.

1. **Admin**
    - Admins can Create, Read, Edit, Delete  Add to favorites and Buy all books on the site. Only admins can see all users and managers.
2. **Manager**
    - Manager can Create and Readall books for sele. Manager can only Edit and Delete only books they have uploaded for sale.
3. **Normal User**
    - Normal users can Read, Add to favorites and Buy all books on the site. They can to become a manager by entering their phone number.
4. **Unkown User**
    - Unregistered users can view all books. They can also view book sales statistics by genre.
  
##  :electric_plug: Installation
To access the project, download the project's zip file and open it with Visual Studio or another IDE. Ensure you have SQL Server Management Studio (SMSS) installed. Add a connection string to the "Manage Secrets JSON". This step will change once the application is deployed. Create initial migration via the "package manager Control" to see the test data. 
You can use the following commands:

**Add-Migration initial**

**Update-Database**

##  :key: Credentials
Those users are seeded into the DB once the initial migration is applied. 
 - Admin User:

   -- UserName: admin@bookshop.bg
   
   -- Password: Admin123456
   
- Manager User:

   -- UserName: Pesho123@softuni.bg
  
   -- Password: Pesho123456
  
- Normal User:

   -- UserName: angelp123@softuni.bg
  
   -- Password: Angel123456

## :hammer: Build With
- ASP.NET Core 6
  -- Database layer with 8 entity models. The "Attractions" entity is implemented on the DB level, but it has to be added to the service and the UI layers.
  -- UI layer with 6 controllers + 1 more in the “Admin” area.
  -- Service layer with 7 services.
  -- Test layer for services and controllers with 28 tests
  -- 25+ Views.
- Entity Framework Core
- Microsoft SQL Server
- HTML&CSS
- TempData messages
- NUnit

##  :camera: Gallery
Unknown User:  Home View
![image](https://github.com/AleksManolow/ASP.NET-Core-Book-Shop/assets/104732760/a0667761-4e1a-4c62-94ac-9b71bbc668ad)

The Unknown User can see All Books.
![image](https://github.com/AleksManolow/ASP.NET-Core-Book-Shop/assets/104732760/2ae843db-5bb2-4f69-8046-a6ce7159c3fa)

The Unknown User can see Book Details.
![image](https://github.com/AleksManolow/ASP.NET-Core-Book-Shop/assets/104732760/950540f9-7475-416b-8d3c-5ba2ebfcae7c)

Register View: 
![image](https://github.com/AleksManolow/ASP.NET-Core-Book-Shop/assets/104732760/1ddd2403-8097-4e34-bc8a-be77d11946bc)

Login View:
![image](https://github.com/AleksManolow/ASP.NET-Core-Book-Shop/assets/104732760/55066d80-b98d-4c7b-a0ae-b4edef3c33dc)

