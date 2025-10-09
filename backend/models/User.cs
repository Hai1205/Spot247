using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("Users")]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    [Required]
    public string Username { get; set; } = "";

    [MaxLength(255)]
    [Required]
    public string Password { get; set; } = "";

    [MaxLength(255)]
    public string FullName { get; set; } = "";

    public UserRole Role { get; set; } = UserRole.STAFF;
}

public enum UserRole
{
    ADMIN,
    STAFF
}