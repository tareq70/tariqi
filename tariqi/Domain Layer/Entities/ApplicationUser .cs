using Microsoft.AspNetCore.Identity;
using System.Reflection;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; } 
        public DateTime? BirthDate { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? AreaId { get; set; }
    }
}
