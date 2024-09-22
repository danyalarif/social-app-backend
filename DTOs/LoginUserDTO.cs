using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.DTOs;

public class LoginUserDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}
