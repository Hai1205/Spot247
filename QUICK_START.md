# 🚀 Quick Start Guide

## ⚡ Setup Nhanh (3 phút)

### 1. Chuẩn Bị
- ✅ .NET 9 SDK
- ✅ MySQL (Laragon)
- ✅ Git

### 2. Clone & Setup
```bash
git clone <repository-url>
cd Spot247
```

### 3. Chạy Script Tự Động
```powershell
.\setup-database.ps1
```

### 4. Chạy Ứng Dụng
```bash
# Backend API
cd backend
dotnet run

# Frontend (terminal mới)
cd frontend  
dotnet run
```

## 🎯 Kết Quả Mong Đợi

- ✅ Database `store_management` được tạo
- ✅ 10 bảng với dữ liệu mẫu
- ✅ API chạy tại `http://localhost:5000`
- ✅ Frontend chạy tại `http://localhost:5001`

## 📊 Dữ Liệu Mẫu

- 👥 **3 users**: admin, staff01, staff02
- 🛍️ **20 customers**: Khách hàng 1-20
- 📦 **50 products**: Sản phẩm đa dạng
- 🛒 **30 orders**: Đơn hàng mẫu
- 🎁 **5 promotions**: Mã khuyến mãi

## 🔧 Nếu Gặp Lỗi

### Lỗi MySQL
```bash
# Kiểm tra MySQL đang chạy
mysql -u root -e "SELECT 1;"
```

### Lỗi .NET
```bash
# Kiểm tra .NET version
dotnet --version
```

### Reset Database
```bash
cd backend
dotnet ef database drop --force
dotnet ef database update
dotnet run
```

## 📞 Cần Hỗ Trợ?

Xem file `README.md` để hướng dẫn chi tiết hơn.

---
**⏱️ Thời gian setup: ~3 phút**  
**🎯 Độ khó: Dễ**  
**📋 Yêu cầu: .NET 9 + MySQL**
