namespace Backend.Dtos.Systems;

public class Request
{
    // Auth
    public string Username { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
    public string Role { get; set; } = "";
    public string Otp { get; set; } = "";
}