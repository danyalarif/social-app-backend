using System;
using System.ComponentModel.DataAnnotations;

namespace social_app_backend.Models;

public class Request
{
    [Key]
    public required int Id {get; set;}
    public required int SenderId { get; set; }
    public required int ReceiverId { get; set; }
  
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
