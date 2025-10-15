# Backend API - Store Management

Backend API được xây dựng với .NET 9, Entity Framework Core và MySQL.

## 🛠️ Setup Database

### 1. Chuẩn Bị MySQL

#### Tạo Database
```sql
CREATE DATABASE store_management;
```

#### Hoặc sử dụng file SQL có sẵn
```bash
mysql -u root -p < store_management.sql
```

### 2. Cấu Hình Connection String

File `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=store_management;Uid=root;Pwd=;"
  }
}
```

### 3. Cài Đặt Dependencies

```bash
# Cài đặt EF tools
dotnet tool install --global dotnet-ef

# Restore packages
dotnet restore
```

### 4. Chạy Migration

#### Tự động (Khuyến nghị)
```bash
dotnet run
```
Ứng dụng sẽ tự động tạo database và seed data.

#### Thủ công
```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update

# Chạy ứng dụng
dotnet run
```

## 📊 Dữ Liệu Mẫu

### Users
- **admin** / 123456 (Quản trị viên)
- **staff01** / 123456 (Nguyễn Văn A)
- **staff02** / 123456 (Lê Thị B)

### Sản Phẩm Mẫu
- 50 sản phẩm đa dạng
- 5 danh mục: Đồ uống, Bánh kẹo, Gia vị, Đồ gia dụng, Mỹ phẩm
- 3 nhà cung cấp: ABC, XYZ, 123

### Đơn Hàng Mẫu
- 30 đơn hàng
- 20 khách hàng
- 5 mã khuyến mãi

## 🔧 Commands Hữu Ích

### Migration Commands
```bash
# Tạo migration mới
dotnet ef migrations add <MigrationName>

# Cập nhật database
dotnet ef database update

# Xóa migration cuối
dotnet ef migrations remove

# Xóa database
dotnet ef database drop --force

# Xem migrations
dotnet ef migrations list
```

### Build & Run
```bash
# Build project
dotnet build

# Run project
dotnet run

# Run với hot reload
dotnet watch run
```

### Reset Database Hoàn Toàn
```bash
# Xóa database và migrations
dotnet ef database drop --force
dotnet ef migrations remove

# Tạo lại từ đầu
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

## 🗂️ Cấu Trúc Code

```
backend/
├── data/
│   └── SeedData.cs              # Dữ liệu mẫu
├── models/                      # Entity models
│   ├── User.cs
│   ├── Customer.cs
│   ├── Product.cs
│   ├── Order.cs
│   └── ...
├── contexts/
│   └── AppDbContext.cs          # Database context
├── controllers/                 # API controllers
│   └── AuthController.cs
├── services/                    # Business logic
│   ├── AuthService.cs
│   └── UserService.cs
├── repositories/                # Data access
│   └── UserRepository.cs
├── dtos/                        # Data transfer objects
├── utils/                       # Utilities
├── configs/                     # Configuration
├── Migrations/                  # EF migrations
└── Program.cs                   # Entry point
```

## 🔍 Kiểm Tra Database

### Sử dụng MySQL Command Line
```bash
mysql -u root -p store_management

# Kiểm tra số lượng records
SELECT COUNT(*) FROM users;
SELECT COUNT(*) FROM products;
SELECT COUNT(*) FROM orders;
```

### Sử dụng phpMyAdmin
1. Mở http://localhost/phpmyadmin
2. Chọn database `store_management`
3. Kiểm tra các bảng và dữ liệu

## 🚨 Troubleshooting

### Lỗi Connection
```
MySqlConnector.MySqlException: Unable to connect
```
**Giải pháp:**
- Kiểm tra MySQL đang chạy
- Kiểm tra connection string
- Kiểm tra port 3306

### Lỗi Migration
```
Could not execute because the specified command or file was not found
```
**Giải pháp:**
```bash
dotnet tool install --global dotnet-ef
```

### Lỗi Table đã tồn tại
```
Table 'users' already exists
```
**Giải pháp:**
```bash
dotnet ef database drop --force
dotnet ef database update
```

### Lỗi Build
```
error CS1061: 'User' does not contain a definition for 'Id'
```
**Giải pháp:**
- Kiểm tra model User có property UserId
- Rebuild project: `dotnet clean && dotnet build`

## 📝 Logs

Khi chạy `dotnet run`, bạn sẽ thấy:
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (Xms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE `users` (...)

✅ Seed data đã được thêm thành công!
```

## 🔄 Workflow Development

1. **Thay đổi Model** → Tạo migration → Update database
2. **Thêm dữ liệu mới** → Cập nhật SeedData.cs
3. **Test API** → Sử dụng Swagger UI tại `/swagger`

## 📞 Hỗ Trợ

Nếu gặp vấn đề:
1. Kiểm tra .NET 9 SDK
2. Kiểm tra MySQL connection
3. Xem logs trong console
4. Reset database nếu cần
