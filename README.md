# Api_.NET8_Dapper

A lightweight and clean Web API built using **ASP.NET Core 8**, **Dapper**, and **Clean Architecture** principles. This project demonstrates a modular and scalable structure for CRUD-based systems, with a focus on simplicity and performance.

---

## 🔧 Tech Stack

- **ASP.NET Core 8**
- **Dapper** – Lightweight ORM for high-performance SQL
- **SQL Server**
- **Clean Architecture**
- **Swagger** – API documentation
- **AutoMapper**
- **FluentValidation**

---

## 📁 Project Structure

Src/
├── api_net8.Application # Application logic (CQRS, DTOs, services)
├── api_net8.Domain # Domain entities and core models
├── api_net8.Infrastructure # Dapper repositories and database access
├── api_.net8.Common # Common utilities and shared configs
Test/
├── ... # Unit/Integration tests


---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/RzSjDev/Api_.net8_Dapper.git
   
2.Navigate to the source directory
dotnet run --project api_net8.Infrastructure

3.Run the API
dotnet run --project api_net8.Infrastructure

4.Open Swagger
Visit https://localhost:5001/swagger to explore the API endpoints.

🧪 Testing
The Test folder contains test projects using xUnit or similar libraries to ensure application correctness. Run tests via:
dotnet test
✅ Features
Modular Clean Architecture

Dapper-based data access for high performance

DTOs with AutoMapper

Validations using FluentValidation

Swagger UI documentation

Easy scalability and maintainability

📄 License
This project is licensed under the MIT License. See the LICENSE.txt for details.




