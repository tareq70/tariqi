# 🚗 TARIQI
## Ride Sharing & Inter-Region Transportation Management System

A scalable ASP.NET Core Web API that organizes inter-region transportation by connecting passengers with drivers, managing trips, bookings, and real-time communication.
Designed to solve transportation coordination issues in rural and regional areas through a modern, secure, and structured system.

Built using Clean Architecture principles to ensure maintainability, testability, and scalability.

## 🚀 Features

    ✅ Secure authentication using ASP.NET Identity
    
    ✅ JWT authentication with Refresh Token rotation
    
    ✅ Multi-role system (Admin, Region Manager, Area Manager, Driver, Passenger)
    
    ✅ Region & Area based transportation structure
    
    ✅ Drivers can create inter-region trips
    
    ✅ Passengers can search, book, cancel, and pay for trips
    
    ✅ Seat availability & booking status management
    
    ✅ Real-time chat using SignalR
    
    ✅ Swagger UI for API testing & documentation

## 👥 User Roles

| Role           | Description                        |
| -------------- | ---------------------------------- |
| Admin          | Full system control and management |
| Region Manager | Manages regions and related areas  |
| Area Manager   | Manages area-level operations      |
| Driver         | Creates trips and manages vehicles |
| Passenger      | Searches and books trips           |

## 🌍 Core Modules

| Module              | Responsibility                                 |
| ------------------- | ---------------------------------------------- |
| Auth & Users        | Registration, login, JWT & refresh tokens      |
| Regions & Areas     | Geographic hierarchy management                |
| Vehicles & Drivers  | Vehicle registration and assignment            |
| Trips               | Create, search, and manage trips               |
| Bookings & Payments | Seat reservation, cancellation, payment status |
| Chats               | Real-time communication using SignalR          |

##  🧩 Code Architecture

### Domain

  - Entities (ApplicationUser, Trip, Booking, Vehicle)

  - Enums (UserRole, Gender)

  - Repository Interfaces

### Application

  - DTOs

  - Service Interfaces (IAuthService, ITripService)

  - Business Logic

### Infrastructure

  - AppDbContext

  - EF Core Migrations

  - ASP.NET Identity Configuration

  - JWT & Token Services

### Presentation (API)

  - Controllers

  - SignalR Hubs

  - API Endpoints

  - Swagger Configuration


## 🔗 Sample API Endpoints

### Authentication

    POST /api/auth/register/{role}
    
    POST /api/auth/login
    
    POST /api/auth/refresh-token


### Trips

    POST /api/trips
    
    GET /api/trips?originRegionId=&destinationRegionId=


### Bookings

    POST /api/trips/{tripId}/book
    
    POST /api/bookings/{id}/pay


## 🛠️ Technologies Used
    ASP.NET Core 10
    
    C#
    
    Entity Framework Core
    
    SQL Server
    
    ASP.NET Identity
    
    JWT Authentication
    
    SignalR

    Swagger / OpenAPI / Postman

    Clean Architecture
    
    Unit of Work With Generic Repository Patterns

## ⚙️ How to Run
    
    ### 1️⃣ Clone the Repository
    git clone https://github.com/your-username/tariqi.git
    
    ### 2️⃣ Configure Application Settings
        Update appsettings.json:
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=...;Database=TariqiDb;Trusted_Connection=True;Encrypt=False"
          },
          "Jwt": {
            "Key": "YOUR_STRONG_JWT_SECRET_KEY",
            "Issuer": "Tariqi.API",
            "Audience": "Tariqi.Client"
          }
        }
    
    ### 3️⃣ Apply Database Migrations
    ### 4️⃣ Run the Application

## 📈 Possible Extensions
    
    Mobile application integration (Flutter / React Native)
    
    Online payment gateway integration
    
    Trip ratings & reviews
    
    Admin dashboard (Web UI)
    
    Unit & integration tests
    
    Push notifications
    
    Microservices version

## 📌 Project Purpose

    Tariqi aims to digitize and organize informal regional transportation by replacing phone-based coordination with a structured, scalable platform suitable for real-world deployment.

### If you want next:

    Architecture diagram
    
    ERD database diagram
    
    Postman collection
    
    CV-ready project description

Just tell me 👌

## 👨‍💻 Authors

Developed by  
**Ahmed Refaat** & **Tarek Elsapagh**

### 🚀 Crafted with care to deliver a clean, scalable, and beautiful solution.


