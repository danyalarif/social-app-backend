using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
