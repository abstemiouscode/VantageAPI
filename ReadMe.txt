# Vantage API

## Overview
This API provides CRUD operations for managing customers and their contacts. It is built using ASP.NET Core 6 and utilizes an InMemory database for storage. The project includes unit tests written with NUnit to verify the functionality of the API endpoints.

## Setup Instructions

### Prerequisites
- [.NET Core 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Cloning the Repository
```sh
git clone <your-repo-url>
cd VantageApi
dotnet build
dotnet run

The API will be available at https://localhost:7100.

Running the Tests
cd VantageApiTests
dotnet test

API Endpoints
GET /api/customers
Retrieves a list of all customers.

GET /api/customers/{id}
Retrieves a specific customer by ID.

POST /api/customers
Creates a new customer.

{
  "name": "string",
  "address": "string",
  "country": "string",
  "phoneNumber": "string",
  "website": "string",
  "contacts": [
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string",
      "phoneNumber": "string"
    }
  ]
}

PUT /api/customers/{id}
Updates an existing customer by ID.

Request Body: Same as POST
DELETE /api/customers/{id}
Deletes a customer by ID.