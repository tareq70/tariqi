using System.ComponentModel.DataAnnotations;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.DTOs.Auth_DTOs
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? EmailConfirmation { get; set; }
    }
}