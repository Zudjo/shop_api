
# Shop API

An interview test by the company Contech.

&nbsp;

# Technical choices

## Frameworks

I chose to use **.NET** and **Angular** because they were preferred.

## Database

I chose **Mysql** because it's relational and widely used.

### Tables

**Invoices**

| **Id** |
|--------|
| 1      |
| 2      |
| . . .  |

**Products**

| **Id** | **Name** | **Price** |
|--------|----------|-----------|
| 1      | Pizza    | 5         |
| 2      | Panino   | 4         |
| . . .  | . . .    | . . .     |

**InvoicesProducts**

| **IdInvoice** | **IdProduct** | **Quantity** |
|---------------|---------------|--------------|
| 1             | 2             | 3            |
| 2             | 2             | 1            |
| . . .         | . . .         | . . .        |

# How to run it

## Set up the database

I used XAMPP to set my local database, but any database you can use it's fine. If you need to change the connection string, you can find it in `/Data/DbTalker.cs`.

## Import the database

In the root folder, you will find the database file `shop_api.sql`, which you can use to import the tables in your database.

## Run it

In the root folder, execute the command `dotnet run`. It will build and execute the program.

Navigate to http://localhost:5153. After a few seconds you will get redirected to the client server.

# Client server

The client server allows you to see the all the invoices, the products, and the invoices with relative products, quantity and total.

# Api server

The api server allows you to CRUD all the tables.

To see and test all the endpoints, navigate to http://localhost:5153/swagger.

*NOTE: You can't delete an invoice or a product if their id is present in invoices-products; because they are linked with a foreign key.*