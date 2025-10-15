namespace Backend.Services;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<User?> HandleCreateUser(string username, string password, string role)
    {
        var existingUser = await _userRepository.HandleGetUserByUsername(username);
        if (existingUser != null)
            return null;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Username = username,
            Password = hashedPassword,
            Role = role ?? "staff",
        };

        return await _userRepository.HandleCreateUser(newUser);
    }

    public async Task<User?> HandleGetUserByUsername(string username)
    {
        return await _userRepository.HandleGetUserByUsername(username);
    }

    public async Task HandleUpdateUserPassword(User user, string newPassword)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.HandleUpdateUser(user);
    }
}
