using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!; // المستلم
        public NotificationType Type { get; set; } 
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
    }
}
