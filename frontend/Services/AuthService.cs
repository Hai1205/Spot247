using Frontend.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Frontend.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigation;
    private UserDto? _currentUser;
    private bool _isLoading = false;

    public AuthService(HttpClient httpClient, NavigationManager navigation)
    {
        _httpClient = httpClient;
        _navigation = navigation;
    }

    public UserDto? CurrentUser => _currentUser;
    public bool IsLoading => _isLoading;
    public bool IsAuthenticated => _currentUser != null;

    public event Action? OnAuthStateChanged;

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();

    /// <summary>
    /// Đăng nhập người dùng
    /// </summary>
    public async Task<ApiResponse?> LoginAsync(LoginRequest request)
    {
        try
        {
            _isLoading = true;
            NotifyAuthStateChanged();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(request.Username), "Username");
            formData.Add(new StringContent(request.Password), "Password");

            var response = await _httpClient.PostAsync("api/v1/auth/login", formData);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response.IsSuccessStatusCode && apiResponse != null)
            {
                _currentUser = apiResponse.Data.User;
                NotifyAuthStateChanged();
            }

            return apiResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return new ApiResponse
            {
                Message = "Đã có lỗi xảy ra trong quá trình đăng nhập",
                StatusCode = 500
            };
        }
        finally
        {
            _isLoading = false;
            NotifyAuthStateChanged();
        }
    }

    /// <summary>
    /// Đăng ký người dùng mới
    /// </summary>
    public async Task<ApiResponse?> RegisterAsync(RegisterRequest request)
    {
        try
        {
            _isLoading = true;
            NotifyAuthStateChanged();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(request.Username), "Username");
            formData.Add(new StringContent(request.Password), "Password");
            formData.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            formData.Add(new StringContent(request.Role), "Role");

            var response = await _httpClient.PostAsync("api/v1/auth/register", formData);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Register error: {ex.Message}");
            return new ApiResponse
            {
                Message = "Đã có lỗi xảy ra trong quá trình đăng ký",
                StatusCode = 500
            };
        }
        finally
        {
            _isLoading = false;
            NotifyAuthStateChanged();
        }
    }

    /// <summary>
    /// Gửi mã OTP đến email
    /// </summary>
    public async Task<ApiResponse?> SendOtpAsync(string email)
    {
        try
        {
            _isLoading = true;
            NotifyAuthStateChanged();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(email), "Username");

            var response = await _httpClient.PostAsync("api/v1/auth/send-otp", formData);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Send OTP error: {ex.Message}");
            return new ApiResponse
            {
                Message = "Đã có lỗi xảy ra khi gửi mã OTP",
                StatusCode = 500
            };
        }
        finally
        {
            _isLoading = false;
            NotifyAuthStateChanged();
        }
    }

    /// <summary>
    /// Xác thực mã OTP
    /// </summary>
    public async Task<ApiResponse?> VerifyOtpAsync(VerifyOtpRequest request)
    {
        try
        {
            _isLoading = true;
            NotifyAuthStateChanged();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(request.Username), "Username");
            formData.Add(new StringContent(request.Otp), "Otp");

            var response = await _httpClient.PostAsync("api/v1/auth/verify-otp", formData);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Verify OTP error: {ex.Message}");
            return new ApiResponse
            {
                Message = "Đã có lỗi xảy ra khi xác thực mã OTP",
                StatusCode = 500
            };
        }
        finally
        {
            _isLoading = false;
            NotifyAuthStateChanged();
        }
    }

    /// <summary>
    /// Đặt lại mật khẩu
    /// </summary>
    public async Task<ApiResponse?> ResetPasswordAsync(ResetPasswordRequest request)
    {
        try
        {
            _isLoading = true;
            NotifyAuthStateChanged();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(request.Username), "Username");
            formData.Add(new StringContent(request.Password), "Password");
            formData.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");

            var response = await _httpClient.PostAsync("api/v1/auth/forgot-password", formData);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Reset password error: {ex.Message}");
            return new ApiResponse
            {
                Message = "Đã có lỗi xảy ra khi đặt lại mật khẩu",
                StatusCode = 500
            };
        }
        finally
        {
            _isLoading = false;
            NotifyAuthStateChanged();
        }
    }

    /// <summary>
    /// Đăng xuất người dùng
    /// </summary>
    public async Task LogoutAsync()
    {
        try
        {
            await _httpClient.PostAsync("api/v1/auth/logout", null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout error: {ex.Message}");
        }
        finally
        {
            _currentUser = null;
            NotifyAuthStateChanged();
            _navigation.NavigateTo("/login");
        }
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    public async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("api/v1/auth/refresh-token", null);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Refresh token error: {ex.Message}");
            return false;
        }
    }
}