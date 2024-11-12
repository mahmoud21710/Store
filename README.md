# Talabat APIs Clone - E-Commerce API System

This project is an **E-Commerce API system** developed to simulate key features of an e-commerce platform like Talabat. The API provides functionalities for managing **Products**, **Users**, **Brands**, **Product Types**, **Orders**, **Payments**, **Tokens**, and **Shopping Baskets**. It is built using **C#** and follows modern software engineering practices, including **Object-Oriented Programming (OOP)**, **N-Tier Architecture**, and various design patterns such as the **Repository Pattern** and **UnitOfWork**.

## Features

- **User Management:** Authentication, registration, and role-based authorization using JWT tokens and Identity (Roles).
- **Product Management:** CRUD operations for products, including product types and brands.
- **Order Management:** Creating, updating, and viewing orders.
- **Payment Integration:** Integration with **Stripe** for handling payments.
- **Basket:** Users can add items to their shopping basket and proceed to checkout.
- **Cache:** Use of **Redis Cache** to store frequently accessed data for improved performance.
- **Error Handling:** Centralized error handling middleware for consistent and detailed error responses.
- **Data Access:** Implements **Entity Framework** and **LINQ** for querying and managing data.
- **AutoMapper:** Simplifies the mapping between different object models (DTOs and entities).
- **Security:** Utilizes **JWT Tokens** for secure authentication and authorization.
- **Unit Testing & Dependency Injection:** Testable design using Dependency Injection and separation of concerns.

## Tools & Technologies

- **C#**: Primary programming language for API development.
- **ASP.NET Core**: Framework for building web APIs.
- **Entity Framework Core**: ORM for data access.
- **LINQ**: Language Integrated Query to simplify data manipulation.
- **Stripe API**: For handling online payments.
- **Redis**: Caching solution to improve application performance.
- **JWT (JSON Web Tokens)**: For secure user authentication.
- **Identity Framework**: Role-based user management.
- **AutoMapper**: To map between domain models and DTOs.
- **Repository Pattern**: Data access abstraction.
- **UnitOfWork**: Pattern for managing transactions.
- **Error Handling Middleware**: For consistent API error responses.
  
## Project Structure

```
/src
    /API
        - Controllers (API endpoints)
        - Middleware (Error handling middleware)
    /Application
        - Services (Business logic)
        - DTOs (Data Transfer Objects)
        - Mappers (AutoMapper profiles)
    /Domain
        - Models (Entities such as User, Product, Order, etc.)
        - Repositories (Data access layer)
        - Interfaces (Contracts for services/repositories)
    /Infrastructure
        - Data (Database context, migrations)
        - Cache (Redis caching configuration)
        - Stripe (Stripe payment integration)
    /Common
        - Helpers (Utility classes)
        - Constants (Configuration and settings)
    /Tests
        - Unit tests for various components (Services, Repositories)
```

## Setup and Installation

### Prerequisites

- **.NET 6.0+** SDK
- **SQL Server** or **SQLite** (or any database of your choice)
- **Redis** (for caching)
- **Stripe API Key** (for payment integration)

### Steps to Run the Project Locally

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/talabat-apis-clone.git
   ```

2. **Install dependencies:**

   Navigate to the project folder and run:

   ```bash
   dotnet restore
   ```

3. **Configure your database:**

   Set up your database connection string in the `appsettings.json` file:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Your-Database-Connection-String"
   }
   ```

4. **Migrate the database:**

   If you're using Entity Framework, apply migrations to create the database schema:

   ```bash
   dotnet ef database update
   ```

5. **Configure Redis (optional but recommended for caching):**

   Set up Redis and add the connection string to `appsettings.json`:

   ```json
   "Redis": {
     "ConnectionString": "Your-Redis-Connection-String"
   }
   ```

6. **Configure Stripe (optional for payment processing):**

   Get your Stripe API keys and add them to the `appsettings.json`:

   ```json
   "Stripe": {
     "SecretKey": "your-stripe-secret-key",
     "PublishableKey": "your-stripe-publishable-key"
   }
   ```

7. **Run the application:**

   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5000` (default).

8. **Test the API:**

   You can use **Postman** or **Swagger UI** to test the endpoints. Swagger is available at `http://localhost:5000/swagger`.

## API Endpoints

- **POST** `/api/auth/register`: Register a new user.
- **POST** `/api/auth/login`: Login and get a JWT token.
- **GET** `/api/products`: Get a list of products.
- **GET** `/api/products/{id}`: Get a single product by ID.
- **POST** `/api/orders`: Create a new order.
- **POST** `/api/payment`: Process a payment with Stripe.
- **GET** `/api/basket`: Get items in the shopping basket.
- **POST** `/api/basket`: Add an item to the basket.

## Contributing

We welcome contributions to this project! To contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit them (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new pull request.
