# Talabat - E-Commerce API

A fully-featured e-commerce REST API built with ASP.NET Core 8 following Clean Architecture principles.

## Tech Stack

- **ASP.NET Core 8** — Web API framework
- **Entity Framework Core** — ORM with SQL Server
- **ASP.NET Core Identity** — User management and roles
- **JWT Bearer Authentication** — Stateless authentication
- **Redis (StackExchange.Redis)** — Basket storage and caching
- **Stripe.net** — Payment intent creation and webhook handling
- **AutoMapper** — Object mapping
- **Swagger / Swashbuckle** — API documentation with JWT support

## Architecture

The solution follows Clean Architecture and is split into 7 projects:
├── Domain            → Entities, contracts (interfaces), exceptions
├── Persistence       → EF Core DbContext, repositories, migrations, Redis
├── Services          → Business logic implementations
├── ServicesAbstraction → Service interfaces
├── Presentation      → Controllers
├── Shared            → DTOs, enums, error models, parameters
└── Learning APIs     → Entry point, middleware, DI extensions
## Features

- **Products** — Listing with filtering by brand/type, search, sorting, and pagination
- **Authentication** — Register, login, get current user, update address
- **Basket** — Create, update, and delete basket stored in Redis
- **Orders** — Create order from basket, get orders by user, delivery methods
- **Payments** — Stripe payment intent creation and webhook status updates
- **Global Exception Handling** — Centralized middleware with domain exception hierarchy
- **Validation** — Custom model state validation response factory
- **Data Seeding** — Products, brands, types, delivery methods, roles, and admin users seeded on startup

## Design Patterns

- Repository Pattern (Generic)
- Unit of Work
- Specification Pattern
- Service Manager
- Factory Pattern (API response)

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Redis
- Stripe account (for payments)

### Setup

1. Clone the repository
```bash
git clone https://github.com/Ahmed-Osaama/Talabat
```

2. Add your configuration in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your_sqlserver_connection_string",
    "IdentityConnection": "your_identity_connection_string",
    "RedisConnection": "localhost"
  },
  "JwtOptions": {
    "SecretKey": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "ExpireInDays": 7
  },
  "StripeSettings": {
    "SecretKey": "your_stripe_secret_key",
    "WebhookSecret": "your_stripe_webhook_secret"
  },
  "URLS": {
    "BaseURL": "https://localhost:port/"
  }
}
```

3. Run the project — migrations and data seeding run automatically on startup.
4. (REDIS MUST BE INSTALLED TO USE THE DATA CORRECTLY).

## API Endpoints

| Module | Method | Endpoint |
|---|---|---|
| Products | GET | `/api/products` |
| Products | GET | `/api/products/{id}` |
| Products | GET | `/api/products/brands` |
| Products | GET | `/api/products/types` |
| Auth | POST | `/api/authentication/register` |
| Auth | POST | `/api/authentication/login` |
| Auth | GET | `/api/authentication` |
| Auth | GET | `/api/authentication/address` |
| Auth | PUT | `/api/authentication/address` |
| Basket | GET | `/api/basket` |
| Basket | POST | `/api/basket` |
| Basket | DELETE | `/api/basket/{id}` |
| Orders | POST | `/api/orders` |
| Orders | GET | `/api/orders` |
| Orders | GET | `/api/orders/{id}` |
| Orders | GET | `/api/orders/deliverymethods` |
| Payments | POST | `/api/payments/{basketId}` |
| Payments | POST | `/api/payments/webhook` |
