using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService, UserService userService) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly UserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] Request request)
    {
        var response = await _authService.Register(request);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] Request request)
    {
        var response = await _authService.Login(request);

        if (response.Data.User != null)
        {
            var accessToken = _authService.HandleGenerateAccessToken(response.Data.User);
            var refreshToken = _authService.HandleGenerateRefreshToken(response.Data.User);

            Response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                // SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRY_MINUTES")!)
                )
            });

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                // SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRY_DAYS")!)
                )
            });
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("logout")]
    [Authorize()]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("accessToken");
        Response.Cookies.Delete("refreshToken");

        return Ok(new { Message = "Logged out" });
    }

    [HttpPost("send-otp")]
    [Authorize()]
    public async Task<IActionResult> SendOtp([FromForm] Request request)
    {
        var response = await _authService.SendOtp(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("verify-otp")]
    [Authorize()]
    public async Task<IActionResult> VerifyOtp([FromForm] Request request)
    {
        var response = await _authService.VerifyOtp(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("forgot-password")]
    [Authorize()]
    public async Task<IActionResult> ForgotPassword([FromForm] Request request)
    {
        var response = await _authService.ForgotPassword(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("change-password")]
    [Authorize()]
    public async Task<IActionResult> ChangePassword([FromForm] Request request)
    {
        var response = await _authService.ChangePassword(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("refresh-token")]
    [Authorize()]
    public async Task<IActionResult> Refresh()
    {
        try
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token is missing");

            var principal = _authService.HandleGetPrincipalFromExpiredToken(refreshToken);

            var Username = principal.Identity?.Name;
            if (string.IsNullOrEmpty(Username))
                return Unauthorized("Invalid token claims");

            var user = await _userService.HandleGetUserByUsername(Username);
            if (user == null)
                return Unauthorized("User not found");

            var userDto = UserMapper.MapEntityToDto(user);
            var newAccessToken = _authService.HandleGenerateAccessToken(userDto);

            Response.Cookies.Append("accessToken", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRY_MINUTES")!)
                )
            });

            return NoContent();
        }
        catch
        {
            return Unauthorized("Invalid refresh token");
        }
    }
}