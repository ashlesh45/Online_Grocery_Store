ONLINE GROCERY STORE DATABASE SYSTEM
PROJECT OVERVIEW

This project is a Database Management System designed for an online grocery store. It enables efficient management of products, categories, and suppliers using an Oracle database. The system provides a structured interface to handle core business operations effectively.

FEATURES
Add new products with input validation
Manage product pricing and stock quantity
Maintain category and supplier data
Store product image paths
Prevent duplicate product entries
Ensure safe database transactions
TECHNOLOGIES USED
Language: C# (.NET Windows Forms)
Database: Oracle Database
Connectivity: Oracle Managed Data Access
Development Tool: Visual Studio
DATABASE STRUCTURE
Products
Categories
Suppliers
KEY FUNCTIONALITY
Validates user input before inserting into the database
Uses transactions to maintain data consistency
Dynamically loads categories and suppliers
Handles exceptions and database errors effectively

Reference implementation:

HOW TO RUN
Clone the repository
Open the project in Visual Studio
Ensure Oracle Database is running

Update the connection string:

User Id=system; Password=student; Data Source=localhost:1521/xe;
Build and run the application
WORKFLOW
Enter product details
Select category and supplier
Click Save
Product is stored in the database
VALIDATION RULES
Product ID must be unique
Price must be numeric
Stock must be a valid integer
Product name cannot be empty
FUTURE IMPROVEMENTS
User authentication system
Online ordering functionality
Payment integration
Analytics dashboard
AUTHOR

Ashlesh Mallya

CONCLUSION

This project demonstrates the implementation of a database-driven system for managing real-world business operations efficiently using structured data and transaction handling.
