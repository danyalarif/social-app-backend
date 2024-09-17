using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.DTOs;

public class LoginUserDTO
{
    [Required]
    [EmailAddress]
    public required string Email;
    [Required]
    public required string Password;
}
