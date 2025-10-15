using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<User?> HandleGetUserByUsername(string username)
    {
        try
        {
            return await _context.Users
                .FirstOrDefaultAsync(c => c.Username == username);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> HandleCreateUser(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> HandleUpdateUser(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
