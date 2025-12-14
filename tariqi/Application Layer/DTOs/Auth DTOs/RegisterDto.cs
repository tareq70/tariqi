using System.ComponentModel.DataAnnotations;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.DTOs.Auth_DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required]    
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required] 
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
