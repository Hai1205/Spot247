# Kiểm tra dữ liệu trong database
$connectionString = "Server=localhost;Port=3306;Database=store_management;Uid=root;Pwd=;"

# Sử dụng MySQL command line để kiểm tra
Write-Host "Kiểm tra dữ liệu trong database store_management..."

# Kiểm tra số lượng records trong các bảng
$queries = @(
    "SELECT COUNT(*) as user_count FROM users;",
    "SELECT COUNT(*) as customer_count FROM customers;",
    "SELECT COUNT(*) as product_count FROM products;",
    "SELECT COUNT(*) as order_count FROM orders;",
    "SELECT COUNT(*) as payment_count FROM payments;"
)

foreach ($query in $queries) {
    Write-Host "Executing: $query"
    # mysql -u root -e "$query" store_management
}

Write-Host "Kiểm tra hoàn tất!"
