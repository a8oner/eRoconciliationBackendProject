# eRoconciliationBackendProject

**eRoconciliationBackendProject** is a backend service built with ASP.NET Core that automates electronic reconciliation processes for businesses. It supports customer account reconciliations and BA/BS forms, offering a secure, modular, and scalable architecture.

## 🚀 Key Features

- Customer account reconciliation module
- BA/BS form management
- JWT-based authentication
- Role-based authorization system
- Clean, layered architecture (API, Business, DataAccess, Core, Entities)
- AutoMapper integration
- FluentValidation for request validation
- PostgreSQL database support
- Serilog-based logging

## 🧱 Architecture Overview

This project is built on a layered architecture:

eRoconciliationBackendProject/
│
├── API/ # Controllers and configuration
├── Business/ # Business logic and rules
├── Core/ # Core utilities (e.g., security, extensions)
├── DataAccess/ # Entity Framework and data access logic
├── Entities/ # Domain models and DTOs
├── WebAPI/ # Application entry point and startup setup

## 🛠️ Technologies

- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- PostgreSQL
- AutoMapper
- FluentValidation
- JWT Authentication
- Serilog
- CORS configuration

