using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class RegisterViewModel
{
    [Required]
    [MaxLength(64, ErrorMessage = "The Name cannot exceed 64 characters.")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64, ErrorMessage = "The Surname cannot exceed 64 characters.")]
    public required string Surname { get; set; }

    [Required]
    [MaxLength(64, ErrorMessage = "The Username cannot exceed 64 characters.")]
    public required string Username { get; set; }

    [MaxLength(64, ErrorMessage = "The Country cannot exceed 64 characters.")]
    public required string Country { get; set; }

    [MaxLength(64, ErrorMessage = "The City cannot exceed 64 characters.")]
    public required string City { get; set; }

    [MaxLength(64, ErrorMessage = "The Street cannot exceed 64 characters.")]
    public required string Street { get; set; }

    [Required][EmailAddress] public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required]
    public required bool IsAdmin { get; set; }
}