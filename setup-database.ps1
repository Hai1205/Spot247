# Script tự động setup database cho Store Management System
# Chạy script này với: .\setup-database.ps1

Write-Host "🚀 Bắt đầu setup database Store Management..." -ForegroundColor Green

# Kiểm tra .NET SDK
Write-Host "📋 Kiểm tra .NET SDK..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    Write-Host "✅ .NET Version: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ .NET SDK chưa được cài đặt. Vui lòng cài đặt .NET 9 SDK" -ForegroundColor Red
    exit 1
}

# Kiểm tra MySQL
Write-Host "📋 Kiểm tra MySQL..." -ForegroundColor Yellow
try {
    # Thử kết nối MySQL
    $mysqlTest = mysql -u root -e "SELECT 1;" 2>$null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ MySQL đang chạy" -ForegroundColor Green
    } else {
        Write-Host "❌ MySQL không chạy hoặc không thể kết nối" -ForegroundColor Red
        Write-Host "💡 Hãy khởi động MySQL (Laragon) trước khi chạy script này" -ForegroundColor Yellow
        exit 1
    }
} catch {
    Write-Host "❌ MySQL command không tìm thấy" -ForegroundColor Red
    Write-Host "💡 Hãy cài đặt MySQL hoặc Laragon" -ForegroundColor Yellow
    exit 1
}

# Tạo database
Write-Host "📋 Tạo database store_management..." -ForegroundColor Yellow
try {
    mysql -u root -e "CREATE DATABASE IF NOT EXISTS store_management;" 2>$null
    Write-Host "✅ Database store_management đã tạo" -ForegroundColor Green
} catch {
    Write-Host "❌ Không thể tạo database" -ForegroundColor Red
    exit 1
}

# Cài đặt EF tools
Write-Host "📋 Cài đặt Entity Framework tools..." -ForegroundColor Yellow
try {
    dotnet tool install --global dotnet-ef --version 9.0.10 2>$null
    Write-Host "✅ EF tools đã cài đặt" -ForegroundColor Green
} catch {
    Write-Host "⚠️ EF tools có thể đã được cài đặt" -ForegroundColor Yellow
}

# Di chuyển vào thư mục backend
Write-Host "📋 Chuyển vào thư mục backend..." -ForegroundColor Yellow
Set-Location backend

# Restore packages
Write-Host "📋 Restore NuGet packages..." -ForegroundColor Yellow
try {
    dotnet restore
    Write-Host "✅ Packages đã restore" -ForegroundColor Green
} catch {
    Write-Host "❌ Lỗi khi restore packages" -ForegroundColor Red
    exit 1
}

# Build project
Write-Host "📋 Build project..." -ForegroundColor Yellow
try {
    dotnet build
    Write-Host "✅ Project đã build thành công" -ForegroundColor Green
} catch {
    Write-Host "❌ Lỗi khi build project" -ForegroundColor Red
    exit 1
}

# Chạy migration và seed data
Write-Host "📋 Chạy migration và seed data..." -ForegroundColor Yellow
Write-Host "⏳ Đang tạo bảng và thêm dữ liệu mẫu..." -ForegroundColor Cyan

try {
    # Chạy ứng dụng để tự động migration và seed
    $process = Start-Process -FilePath "dotnet" -ArgumentList "run" -PassThru -NoNewWindow
    $process.WaitForExit(30000) # Đợi tối đa 30 giây
    
    if ($process.ExitCode -eq 0) {
        Write-Host "✅ Migration và seed data hoàn tất!" -ForegroundColor Green
    } else {
        Write-Host "⚠️ Ứng dụng đã dừng, kiểm tra logs ở trên" -ForegroundColor Yellow
    }
} catch {
    Write-Host "❌ Lỗi khi chạy migration" -ForegroundColor Red
}

# Kiểm tra kết quả
Write-Host "📋 Kiểm tra kết quả..." -ForegroundColor Yellow
try {
    $userCount = mysql -u root -e "SELECT COUNT(*) as count FROM store_management.users;" 2>$null | Select-String -Pattern "\d+" | ForEach-Object { $_.Matches[0].Value }
    $productCount = mysql -u root -e "SELECT COUNT(*) as count FROM store_management.products;" 2>$null | Select-String -Pattern "\d+" | ForEach-Object { $_.Matches[0].Value }
    $orderCount = mysql -u root -e "SELECT COUNT(*) as count FROM store_management.orders;" 2>$null | Select-String -Pattern "\d+" | ForEach-Object { $_.Matches[0].Value }
    
    Write-Host "📊 Kết quả seed data:" -ForegroundColor Cyan
    Write-Host "   👥 Users: $userCount" -ForegroundColor White
    Write-Host "   📦 Products: $productCount" -ForegroundColor White
    Write-Host "   🛒 Orders: $orderCount" -ForegroundColor White
    
    if ([int]$userCount -gt 0 -and [int]$productCount -gt 0) {
        Write-Host "✅ Database đã được setup thành công!" -ForegroundColor Green
    } else {
        Write-Host "⚠️ Database có thể chưa được seed đầy đủ" -ForegroundColor Yellow
    }
} catch {
    Write-Host "⚠️ Không thể kiểm tra kết quả tự động" -ForegroundColor Yellow
}

# Quay lại thư mục gốc
Set-Location ..

Write-Host "`n🎉 Setup hoàn tất!" -ForegroundColor Green
Write-Host "📝 Để chạy ứng dụng:" -ForegroundColor Cyan
Write-Host "   Backend: cd backend && dotnet run" -ForegroundColor White
Write-Host "   Frontend: cd frontend && dotnet run" -ForegroundColor White
Write-Host "`n📖 Xem README.md để biết thêm chi tiết" -ForegroundColor Cyan
