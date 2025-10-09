# Backend CORS Configuration

Để Blazor WebAssembly có thể gọi API từ backend, bạn cần cấu hình CORS trong backend ASP.NET Core.

## Cấu hình trong Program.cs của Backend

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddAuthentication();
// ... other services

// Thêm CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins(
                "https://localhost:5001",  // Blazor WASM HTTPS
                "http://localhost:5000",   // Blazor WASM HTTP
                "http://localhost:5120",   // Port có thể thay đổi
                "https://localhost:7120"   // HTTPS port
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Quan trọng: để cookies hoạt động
    });
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Sử dụng CORS (đặt trước UseRouting)
app.UseCors("AllowBlazorWasm");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## Lưu ý quan trọng:

1. **AllowCredentials()** - Bắt buộc để cookies JWT hoạt động
2. **Thứ tự middleware** - CORS phải đặt trước UseRouting
3. **Origins** - Thêm tất cả ports mà Blazor có thể chạy
4. **Development only** - Trong production, chỉ allow domain cụ thể

## Test CORS:

1. Chạy backend: `dotnet run` (thường tại https://localhost:7071)
2. Chạy Blazor: `dotnet run` (tại http://localhost:5120)
3. Kiểm tra Network tab trong browser để xem preflight requests
4. Nếu có lỗi CORS, thêm port của Blazor vào WithOrigins()

## Cập nhật API Base Address:

Trong `Program.cs` của Blazor, đảm bảo base address đúng:

```csharp
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("https://localhost:7071/") // Port của backend
});
```
