using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.DTOs.Auth_DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
    }
}
