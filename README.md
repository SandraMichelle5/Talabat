📦 Talabat -- ASP.NET Core Web API
A Talabat-inspired e-commerce backend API built with ASP.NET Core, designed to practice real-world backend engineering concepts including Clean Architecture, Redis caching, payment integration, and scalable API design.

This project focuses on how production-ready backend systems are structured, not just CRUD endpoints.

🎯 Project Goals
Practice ASP.NET Core Web API development
Apply Clean / Onion Architecture
Use Redis distributed caching
Implement payment workflows
Follow SOLID principles & design patterns
Build a scalable, maintainable backend system
🏗 Architecture Overview
The project follows Clean Architecture (Onion Architecture).

 Talabat
├── Talabat.Core
│   ├── Entities
│   ├── Interfaces
│   └── Specifications
│
├── Talabat.Repository
│   ├── Data (DbContext)
│   ├── Repositories
│   └── Configurations
│
├── Talabat.Service
│   ├── Business Services
│   └── Service Interfaces
│
└── Talabat.APIs
    ├── Controllers
    ├── DTOs
    ├── Middlewares
    └── Extensions
📌 Architectural Principles
Dependency Inversion:
High-level modules depend on abstractions, not implementations

Separation of Concerns:
API, business logic, and data access are isolated

Testability & Maintainability
Framework Independence
🛠 Technologies & Tools
Technology	Purpose
ASP.NET Core	Web API
Entity Framework Core	ORM
SQL Server	Relational database
Redis	Distributed caching
JWT	Authentication
Stripe	Payments
Swagger / OpenAPI	API documentation
AutoMapper	Object mapping
Core Features
🔐 Authentication & Authorization
JWT-based authentication

Secure protected endpoints

Token validation & authorization middleware

📦 Product Catalog
Product listing with filtering

Pagination & sorting

Efficient querying using specifications

🛍 Basket & Orders
Add/remove items from the basket

Create orders from the basket

Retrieve order history

💳 Payments
Payment intent creation

Secure payment processing (e.g., Stripe)

Payment confirmation workflow

Designed to support multiple gateways

🧩 Design Patterns & Practices
Pattern / Practice	Description
Repository Pattern	Abstracts data access logic
Unit of Work	Ensures transactional consistency
Specification Pattern	Encapsulates complex query logic
DTO Pattern	Prevents domain leakage to API
Dependency Injection	Loosely coupled services
Global Exception Handling	Centralized error handling
⚡ Redis Distributed Caching
This project uses Redis as a distributed cache to optimize performance for read-heavy endpoints.

🧠 Why Redis?
Caching helps reduce:

⏱ Response Time
🗄 Database Load
📈 Scalability Risk
🛠 Caching Strategy (Cache-Aside Pattern)
API checks the Redis cache
If cache hit → return cached data
If cache miss → query database and store result in Redis
Flow:

Request → Cache → Database (on miss) → Cache
✨ Implementation Highlights
Uses IDistributedCache abstraction
Redis configured using StackExchange.Redis
Supports absolute and sliding expiration
🔧 Redis Configuration (C#)
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        configuration.GetConnectionString("Redis");
});
✅ Benefits

Faster API responses

Reduced SQL Server queries

Improved scalability

Stateless API design

💳 Payment Idempotency & Concurrency Handling
Payment processing is designed to be safe under retries and concurrent requests.

While Stripe provides idempotency at the API level, this alone does not protect the server from race conditions, duplicate workflows, or concurrent execution.

Problem
Multiple concurrent requests using the same payment intent can:

Trigger duplicate Stripe calls
Cause inconsistent order state
Break retry safety
Solution
The API implements server-side idempotency using Redis:

Atomic distributed locks (SETNX) to prevent concurrent execution
Cached payment results to safely handle retries
Lock expiration (TTL) to prevent deadlocks
Double-check pattern to close race-condition windows
Execution Flow
Check if a payment result already exists
Attempt to acquire a Redis lock for the idempotency key
Re-check the cached result after acquiring the lock
Create Stripe PaymentIntent
Cache the successful payment result
Release the lock
This guarantees that each payment operation is executed exactly once, even under concurrent requests or repeated client retries.

⚙ Getting Started
📋 Prerequisites
.NET SDK (6 / 7 / 8)

SQL Server

Redis

Stripe account (for payments)

📥 Installation
git clone https://github.com/Kareem20/Talabat.git
cd Talabat
🔧 Configuration
Update appsettings.json:

Database connection string

Redis connection string

JWT settings

Stripe API keys

Run the API
dotnet restore
dotnet build
dotnet run --project Talabat.APIs
📐 Design Decisions
✅ Clean Architecture
Core logic is independent of frameworks

Easy to test and extend

Infrastructure can be replaced without affecting business logic

✅ Redis at Infrastructure Layer
No Redis dependency in Core

Cache implementation is replaceable

✅ Payment Design
Isolated payment service

Easy to extend for additional providers

🧠 What This Project Demonstrates
Real-world ASP.NET Core API design

Performance optimization using Redis

Clean, scalable backend architecture

Payment workflows using the Stripe gateway

Professional backend engineering practices

📜 License

This project is created for learning and practice purposes.
