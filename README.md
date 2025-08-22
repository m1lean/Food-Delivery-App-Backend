# 🍔 Food Delivery App Backend

A full-featured backend for a food delivery application similar to **Glovo**, built with **ASP.NET Core** using **Entity Framework Core** and **JWT authentication**.

## 🚀 Features

- **JWT Authentication & Authorization** - Secure user login system
- **Order Management** - Complete order lifecycle from creation to delivery
- **Automatic Courier Assignment** - System automatically finds available couriers
- **Order Status Management** - Real-time status tracking
- **Swagger Integration** - Complete API documentation for testing
- **Database Seeding** - Automatic population with test data

## 🏗️ Architecture

### Core Entities:
- **User** - System users
- **Restaurant** - Restaurants with menus
- **Product** - Restaurant dishes
- **Courier** - Delivery couriers
- **Order** - Orders with status tracking

### Order Status Flow:
```
Pending → Preparing → OnTheWay → Completed
    ↓         ↓          ↓
 Cancelled ← Cancelled ← Cancelled
```

## 🛠️ Technologies

- **ASP.NET Core** - Main framework
- **Entity Framework Core** - ORM for database operations
- **SQLite** - Lightweight database
- **JWT Bearer** - Authentication tokens
- **Swagger/OpenAPI** - API documentation
- **AutoMapper** - Object mapping

## 📋 API Endpoints

### Authentication
```
POST /api/auth/register - Register new user
POST /api/auth/login    - User login
```

### Orders (Authorization required)
```
GET  /api/orders           - Get all orders
POST /api/orders           - Create new order
PUT  /api/orders/{id}/status - Update order status
```

## 🚀 Quick Start

### Requirements
- .NET 8.0 SDK
- Visual Studio 2022 или VS Code

### Installation & Setup

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/food-delivery-backend.git
cd food-delivery-backend
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Run the application**
```bash
dotnet run
```

4. **Open Swagger UI**
```
https://localhost:7000/swagger
```

The database will be automatically created and populated with test data on first run.

## 📖 Usage

### 1. Registration & Login
```json
POST /api/auth/register
{
  "username": "testuser",
  "password": "password123"
}
```

### 2. Create an order
```json
POST /api/orders
Authorization: Bearer <your-jwt-token>
{
  "restaurantId": 1,
  "productIds": [1, 2, 3]
}
```

### 3. Update order status
```json
PUT /api/orders/1/status
{
  "status": "Preparing"
}
```

## 🗃️ Database Structure

```sql
Users: Id, Username, PasswordHash
Restaurants: Id, Name, Address
Products: Id, Name, Price, RestaurantId
Couriers: Id, Name, IsAvailable
Orders: Id, UserId, RestaurantId, CourierId, Status, OrderDate, TotalAmount
```

## 🔧 Configuration

Main settings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=fooddelivery.db"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "FoodDeliveryApp",
    "Audience": "FoodDeliveryUsers",
    "ExpirationHours": 24
  }
}
```

## 📝 Test Data

On first run, the following data is created:
- **3 restaurants** with different cuisines
- **9 dishes** (3 per restaurant)
- **2 couriers** for delivery

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## 🎯 Roadmap

- [ ] Add real-time notifications (SignalR)
- [ ] Payment system integration
- [ ] Courier geolocation and delivery tracking
- [ ] Rating and review system
- [ ] Admin dashboard

## 📞 Contact

- GitHub: [@m1lean]([https://github.com/yourusername](https://github.com/m1lean))
- Email: altnntx@gmail.com

---

⭐ **Give it a star if this project was helpful!**
