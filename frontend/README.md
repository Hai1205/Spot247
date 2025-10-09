# Blazor WebAssembly Authentication App - Spot247

## 📖 Mô tả

Đây là frontend Blazor WebAssembly thay thế cho phần authentication của ứng dụng Next.js. Ứng dụng cung cấp đầy đủ chức năng đăng nhập, đăng ký, quên mật khẩu, và xác thực OTP.

## ✨ Tính năng

- **Đăng nhập** với email và mật khẩu
- **Đăng ký** tài khoản mới
- **Quên mật khẩu** với OTP qua email
- **Xác thực OTP** (6 số) với timer đếm ngược
- **Đặt lại mật khẩu** sau khi xác thực OTP
- **Form validation** với DataAnnotations
- **Loading states** và error handling
- **Responsive design** tương thích mobile
- **Toast notifications** cho UX tốt hơn

## 🏗️ Cấu trúc Project

```
BlazorAuthApp/
├── Pages/
│   ├── Auth/
│   │   ├── Login.razor                 # Trang đăng nhập
│   │   ├── Register.razor              # Trang đăng ký
│   │   ├── ForgotPassword.razor        # Trang quên mật khẩu
│   │   ├── ResetPassword.razor         # Trang đặt lại mật khẩu
│   │   └── VerifyEmail.razor           # Trang xác thực OTP
│   └── Home.razor                      # Trang chủ
│
├── Shared/
│   ├── AuthLayout.razor                # Layout cho auth pages
│   └── Components/
│       ├── InputComponent.razor        # Component input tái sử dụng
│       ├── ButtonComponent.razor       # Component button với loading
│       └── ToastComponent.razor        # Component thông báo
│
├── Services/
│   └── AuthService.cs                  # Service xử lý API authentication
│
├── Models/
│   └── AuthModels.cs                   # Models và DTOs
│
├── wwwroot/
│   ├── css/
│   │   └── auth.css                    # Custom CSS styling
│   └── images/                         # Thư mục hình ảnh
│
├── Program.cs                          # Cấu hình dependency injection
├── App.razor                           # Root component với routing
└── _Imports.razor                      # Global using statements
```

## 🚀 Cài đặt và Chạy

### 1. Cài đặt Dependencies

```bash
cd BlazorAuthApp
dotnet restore
```

### 2. Cấu hình Backend API

Trong file `Program.cs`, cập nhật base address của backend:

```csharp
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("https://localhost:7071/") // Thay bằng địa chỉ backend của bạn
});
```

### 3. Chạy Backend API

Đảm bảo backend ASP.NET Core đang chạy trước:

```bash
cd ../backend
dotnet run
```

### 4. Chạy Blazor WASM

```bash
cd BlazorAuthApp
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:5001`

## 🔧 Cấu hình Backend CORS

Để Blazor WASM có thể gọi API từ backend, cần cấu hình CORS trong backend:

```csharp
// Program.cs trong backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:5001", "http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Sau khi build app
app.UseCors("AllowBlazorWasm");
```

## 📱 Flow Người dùng

### 1. Đăng ký tài khoản mới

- Truy cập `/register`
- Nhập email, mật khẩu, xác nhận mật khẩu
- Submit form → Chuyển đến `/login`

### 2. Đăng nhập

- Truy cập `/login`
- Nhập email và mật khẩu
- Nếu cần xác thực → Chuyển đến `/verify-email`
- Nếu thành công → Chuyển đến trang chủ `/`

### 3. Quên mật khẩu

- Từ trang login, click "Quên mật khẩu?"
- Nhập email → Gửi OTP → Chuyển đến `/verify-email?isPasswordReset=true`
- Xác thực OTP → Chuyển đến `/reset-password`
- Đặt mật khẩu mới → Về `/login`

### 4. Xác thực OTP

- Nhập 6 số OTP nhận từ email
- Timer đếm ngược 5 phút
- Có thể gửi lại OTP sau khi hết thời gian

## 🎨 Styling

Ứng dụng sử dụng:

- **Custom CSS** tương thích với Tailwind CSS classes
- **Dark theme** với gradient background
- **Responsive design** cho mobile
- **Loading animations** và transitions
- **Form validation styling**

## 🔌 API Endpoints

Blazor app gọi các endpoints sau từ backend:

- `POST /api/v1/auth/login` - Đăng nhập
- `POST /api/v1/auth/register` - Đăng ký
- `POST /api/v1/auth/send-otp` - Gửi OTP
- `POST /api/v1/auth/verify-otp` - Xác thực OTP
- `POST /api/v1/auth/forgot-password` - Đặt lại mật khẩu
- `POST /api/v1/auth/logout` - Đăng xuất
- `POST /api/v1/auth/refresh-token` - Refresh token

## 🛠️ Customization

### Thay đổi theme/colors

Chỉnh sửa các CSS classes trong `wwwroot/css/auth.css`

### Thêm validation rules

Cập nhật các attributes trong `Models/AuthModels.cs`

### Thay đổi API endpoints

Chỉnh sửa `Services/AuthService.cs`

### Thêm tính năng mới

Tạo components mới trong `Shared/Components/` và pages trong `Pages/`

## 🚨 Lưu ý

1. **CORS**: Đảm bảo backend có cấu hình CORS cho phép Blazor WASM
2. **HTTPS**: Cần chạy backend với HTTPS để cookies hoạt động đúng
3. **Environment**: Cập nhật base address API trong `Program.cs`
4. **Email**: Backend cần cấu hình SMTP để gửi OTP

## 📞 Hỗ trợ

- Kiểm tra browser console để debug lỗi
- Đảm bảo backend API đang chạy và có thể truy cập
- Kiểm tra network tab để xem requests/responses

## 🎯 Demo UI

Giao diện giống hệt với phiên bản Next.js:

- Dark theme với background gradient
- Form validation với error messages
- Loading states khi gọi API
- Toast notifications
- Responsive mobile design
- Timer countdown cho OTP
- Auto-focus và auto-advance cho OTP inputs
