using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.DTOs;

public class CreateUserDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    [StringLength(30)]
    public string? FirstName { get; set; } 
    [StringLength(30)]
    public string? LastName { get; set; }
}
