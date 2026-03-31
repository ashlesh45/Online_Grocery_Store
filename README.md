Online Grocery Store Database System
Project Overview

This project is a Database Management System designed for an online grocery store. It enables efficient management of products, categories, suppliers, and inventory using an Oracle database. The system provides a structured interface to handle core operations required for a grocery business.

Features
Add new products with proper input validation
Manage product pricing and stock quantity
Maintain category and supplier information
Store product image paths
Prevent duplicate product entries
Ensure safe database transactions
Technologies Used
Language: C# (.NET Windows Forms)
Database: Oracle Database
Connectivity: Oracle Managed Data Access
Development Tool: Visual Studio
Database Tables
Products
Categories
Suppliers
Key Functionality
Input validation before database insertion
Use of transactions to maintain data consistency
Dynamic loading of categories and suppliers
Exception handling for database operations

Reference implementation:

How to Run
Clone the repository
Open the project in Visual Studio
Ensure Oracle Database is running

Update the connection string:

User Id=system; Password=student; Data Source=localhost:1521/xe;
Build and run the application
Sample Workflow
Enter product details
Select category and supplier
Save the product
Data is stored in the database
Validation Rules
Product ID must be unique
Price must be numeric
Stock must be a valid integer
Product name cannot be empty
Future Improvements
User authentication system
Online ordering functionality
Payment integration
Analytics dashboard
Author

Ashlesh Mallya

Conclusion

This project demonstrates the implementation of a database-driven application for managing real-world business operations efficiently using structured data and transaction handling.
