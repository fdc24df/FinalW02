LoanAPI: Secure Loan Application and Management System
This repository contains the source code for a secure Loan Application and Management API built using ASP.NET Core. It provides endpoints for user authentication (registration, login) and core financial operations (loan application and retrieval).

Table of Contents
1.Technology Stack

2.Getting Started

    Prerequisites

    Installation

    Configuration

3.Running the Application

4.API Endpoints

    Authentication

    Loan Management

5.Testing

1. Technology Stack
Framework: ASP.NET Core 8.0 (or latest stable version)

    Language: C#

    Database: SQL Server (via Entity Framework Core)

    ORM: Entity Framework Core

    Testing: xUnit, Moq

    Security: JWT (JSON Web Tokens) Authentication

2. Getting Started
Prerequisites
.NET 8 SDK

Visual Studio 2022 or Visual Studio Code

SQL Server (LocalDB or remote instance)

Installation

    1.Clone the repository

    2.Restore dependencies

    3.Database Configuration: Ensure your SQL Server connection string is correctly defined in appsettings.json.

Configuration

The application uses JWT Bearer Authentication. Ensure the necessary settings are present in appsettings.json:

3. Running the Application
    1.Apply Migrations: Navigate to the project directory that contains the ApplicationDbContext.


    2.Start the API:

    The API will typically run on https://localhost:7000 (port may vary).

4. API Endpoints
The API is documented using Swagger/OpenAPI. Once the application is running, access the documentation page:

Swagger UI Link: https://localhost:[PORT]/swagger

Authentication

POST	/api/auth/register
Registers a new user with a username, email, and password.

POST	/api/auth/login
Authenticates a user and returns a JWT token.

Loan Management
These endpoints require a valid JWT Bearer token in the Authorization header.

POST	/api/loan	Submits a new loan application.

GET	/api/loan/{id}	Retrieves details for a specific loan application by ID.

GET	/api/loan/my-loans	Retrieves all loan applications submitted by the current user.

5. Testing
Unit tests are written using xUnit and Moq to ensure that the core business logic (e.g., AuthService, LoanService) is reliable and error-free.

Run Tests:

    1.Open Test Explorer in Visual Studio.

    2.Select Run All Tests.
