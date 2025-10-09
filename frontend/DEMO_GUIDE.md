# 🚀 Demo và Test Blazor Auth App

## Cách test toàn bộ flow authentication:

### 1. Khởi động Backend API

```bash
cd ../backend
dotnet run
```

Backend sẽ chạy tại: `https://localhost:7071`

### 2. Khởi động Blazor WASM

```bash
cd BlazorAuthApp
dotnet run
```

Frontend sẽ chạy tại: `https://localhost:5001`

### 3. Test các trang

#### 🔐 Trang Đăng ký (`/register`)

- Nhập email hợp lệ (vd: `test@example.com`)
- Nhập mật khẩu (tối thiểu 8 ký tự)
- Xác nhận mật khẩu
- Click "Đăng ký" → Chuyển đến `/login`

#### 🔑 Trang Đăng nhập (`/login`)

- Nhập email và mật khẩu đã đăng ký
- Click "Đăng nhập"
- Nếu cần xác thực → Chuyển đến `/verify-email`
- Nếu thành công → Chuyển đến trang chủ

#### 🔓 Quên mật khẩu (`/forgot-password`)

- Nhập email đã đăng ký
- Click "Gửi mã" → OTP được gửi về email
- Chuyển đến `/verify-email?isPasswordReset=true`

#### ✅ Xác thực OTP (`/verify-email`)

- Nhập 6 số OTP từ email
- Timer đếm ngược 5 phút
- Click "Xác thực"
- Nếu là password reset → Chuyển đến `/reset-password`
- Nếu là account verification → Chuyển đến `/login`

#### 🔄 Đặt lại mật khẩu (`/reset-password`)

- Nhập mật khẩu mới (tối thiểu 8 ký tự)
- Xác nhận mật khẩu mới
- Click "Đặt lại mật khẩu" → Chuyển đến `/login`

### 4. Screenshots của giao diện

Giao diện tương tự Next.js với:

- ✅ Dark theme với gradient background
- ✅ Responsive design cho mobile
- ✅ Form validation với error messages
- ✅ Loading states khi gọi API
- ✅ Toast notifications
- ✅ Timer countdown cho OTP
- ✅ Auto-focus và auto-advance cho OTP inputs

### 5. Kiểm tra Network requests

Mở Developer Tools → Network tab để xem:

- POST requests đến `/api/v1/auth/*` endpoints
- Response status codes
- Cookies được set (accessToken, refreshToken)

### 6. Troubleshooting

#### Lỗi CORS:

- Đảm bảo backend có cấu hình CORS (xem `BACKEND_CORS_CONFIG.cs`)
- Kiểm tra origins trong CORS policy

#### Lỗi 404 API:

- Kiểm tra backend đang chạy tại đúng port
- Cập nhật base address trong `Program.cs`

#### Lỗi validation:

- Kiểm tra format email
- Mật khẩu phải có ít nhất 8 ký tự
- Xác nhận mật khẩu phải trùng khớp

#### Không nhận được OTP:

- Kiểm tra backend có cấu hình SMTP
- Kiểm tra spam folder
- Kiểm tra logs backend

### 7. Demo data để test

**Tài khoản test:**

- Email: `demo@spot247.com`
- Password: `Demo123456!`

**Test flow hoàn chỉnh:**

1. Đăng ký tài khoản mới
2. Đăng nhập → Cần xác thực OTP
3. Verify OTP → Vào được trang chủ
4. Đăng xuất
5. Quên mật khẩu → Gửi OTP
6. Verify OTP → Đặt lại mật khẩu
7. Đăng nhập với mật khẩu mới

---

## 🎯 So sánh với Next.js Version

| Feature             | Next.js | Blazor WASM | Status        |
| ------------------- | ------- | ----------- | ------------- |
| Login page          | ✅      | ✅          | ✅ Hoàn thành |
| Register page       | ✅      | ✅          | ✅ Hoàn thành |
| Forgot password     | ✅      | ✅          | ✅ Hoàn thành |
| Reset password      | ✅      | ✅          | ✅ Hoàn thành |
| OTP verification    | ✅      | ✅          | ✅ Hoàn thành |
| Form validation     | ✅      | ✅          | ✅ Hoàn thành |
| Loading states      | ✅      | ✅          | ✅ Hoàn thành |
| Error handling      | ✅      | ✅          | ✅ Hoàn thành |
| Toast notifications | ✅      | ✅          | ✅ Hoàn thành |
| Responsive design   | ✅      | ✅          | ✅ Hoàn thành |
| Dark theme          | ✅      | ✅          | ✅ Hoàn thành |

**Kết quả: 100% feature parity với Next.js version! 🎉**
