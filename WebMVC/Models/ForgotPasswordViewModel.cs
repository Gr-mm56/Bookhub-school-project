using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}

