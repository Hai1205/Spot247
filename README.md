# Store Management System

Hệ thống quản lý cửa hàng được xây dựng với .NET 9 và MySQL.

## 📋 Yêu Cầu Hệ Thống

- .NET 9 SDK
- MySQL Server (khuyến nghị sử dụng Laragon)
- Visual Studio 2022 hoặc VS Code

## 🚀 Hướng Dẫn Setup

### 1. Clone Repository

```bash
git clone <repository-url>
cd Spot247
```

### 2. Cài Đặt MySQL

#### Sử dụng Laragon (Khuyến nghị)
1. Tải và cài đặt [Laragon](https://laragon.org/)
2. Khởi động Laragon
3. Tạo database mới tên `store_management` trong phpMyAdmin

#### Hoặc MySQL thủ công
```sql
CREATE DATABASE store_management;
```

### 3. Cấu Hình Connection String

Kiểm tra file `backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=store_management;Uid=root;Pwd=;"
  }
}
```

**Lưu ý:** Thay đổi `Pwd` nếu MySQL có password.

### 4. Cài Đặt Entity Framework Tools

```bash
cd backend
dotnet tool install --global dotnet-ef
```

### 5. Chạy Migration và Seed Data

#### Cách 1: Tự động (Khuyến nghị)
```bash
cd backend
dotnet run
```
Ứng dụng sẽ tự động:
- Tạo database nếu chưa có
- Chạy migration
- Thêm dữ liệu mẫu

#### Cách 2: Thủ công
```bash
cd backend

# Tạo migration
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update

# Chạy ứng dụng để seed data
dotnet run
```

### 6. Kiểm Tra Kết Quả

Mở phpMyAdmin và kiểm tra database `store_management` có các bảng:
- `users` (3 records)
- `customers` (20 records) 
- `categories` (5 records)
- `suppliers` (3 records)
- `products` (50 records)
- `inventory` (50 records)
- `promotions` (5 records)
- `orders` (30 records)
- `order_items` (100+ records)
- `payments` (30 records)

## 🗄️ Cấu Trúc Database

### Bảng Chính
- **users**: Quản lý người dùng (admin/staff)
- **customers**: Thông tin khách hàng
- **categories**: Danh mục sản phẩm
- **suppliers**: Nhà cung cấp
- **products**: Sản phẩm
- **inventory**: Tồn kho
- **promotions**: Khuyến mãi
- **orders**: Đơn hàng
- **order_items**: Chi tiết đơn hàng
- **payments**: Thanh toán

### Dữ Liệu Mẫu
- 3 tài khoản người dùng (admin, staff01, staff02)
- 20 khách hàng
- 5 danh mục sản phẩm
- 3 nhà cung cấp
- 50 sản phẩm đa dạng
- 30 đơn hàng mẫu
- 5 mã khuyến mãi

## 🔧 Troubleshooting

### Lỗi Connection String
```
MySqlConnector.MySqlException: Unable to connect to any of the specified MySQL hosts
```
**Giải pháp:** Kiểm tra MySQL đang chạy và connection string đúng.

### Lỗi Migration
```
Could not execute because the specified command or file was not found
```
**Giải pháp:** Cài đặt EF tools:
```bash
dotnet tool install --global dotnet-ef
```

### Lỗi Database đã tồn tại
```
Table 'users' already exists
```
**Giải pháp:** Xóa database và tạo lại:
```bash
dotnet ef database drop --force
dotnet ef database update
```

### Reset Database hoàn toàn
```bash
cd backend
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

## 📁 Cấu Trúc Project

```
Spot247/
├── backend/
│   ├── data/
│   │   └── SeedData.cs          # Dữ liệu mẫu
│   ├── models/                  # Entity models
│   ├── contexts/
│   │   └── AppDbContext.cs      # Database context
│   ├── controllers/             # API controllers
│   ├── services/                # Business logic
│   ├── repositories/            # Data access
│   └── Migrations/              # EF migrations
├── frontend/                    # Blazor frontend
└── README.md
```

## 🚀 Chạy Ứng Dụng

### Backend API
```bash
cd backend
dotnet run
```
API sẽ chạy tại: `http://localhost:5000`

### Frontend Blazor
```bash
cd frontend
dotnet run
```
Frontend sẽ chạy tại: `http://localhost:5001`

## 📝 Ghi Chú

- Database sẽ được tạo tự động khi chạy lần đầu
- Dữ liệu mẫu chỉ được thêm nếu bảng còn trống
- Sử dụng Laragon để dễ dàng quản lý MySQL
- Kiểm tra log console để xem quá trình seed data

## 🆘 Hỗ Trợ

Nếu gặp vấn đề, hãy kiểm tra:
1. .NET 9 SDK đã cài đặt
2. MySQL server đang chạy
3. Connection string đúng
4. Port 3306 không bị chiếm dụng
5. Database `store_management` đã tạo
