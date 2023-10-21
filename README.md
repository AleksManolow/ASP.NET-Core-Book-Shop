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
