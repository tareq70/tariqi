using Microsoft.AspNetCore.Identity;
using System.Reflection;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Passenger;
        public Gender Gender { get; set; } 
        public DateTime? BirthDate { get; set; }
        public int? RegionId { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }


        public Region? Region { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Trip> CreatedTrips { get; set; } = new List<Trip>();
    }
}
