using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.DTOs;

public class CreateRequestDTO
{
    [Required]
    public int ReceiverId {get; set;}
}
