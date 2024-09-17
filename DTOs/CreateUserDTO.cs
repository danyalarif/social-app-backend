using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.DTOs;

public class CreateUserDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [StringLength(30)]
    public string FirstName { get; set; } 
    [StringLength(30)]
    public string LastName { get; set; }
}
