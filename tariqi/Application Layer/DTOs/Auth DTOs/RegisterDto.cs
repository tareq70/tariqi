using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.DTOs.Auth_DTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = "Passenger";
    }
}
