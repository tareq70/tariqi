namespace tariqi.Domain_Layer.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string FromUserId { get; set; } = null!;
        public string ToUserId { get; set; } = null!;
        public int? TripId { get; set; }
        public string MessageText { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser FromUser { get; set; } = null!;
        public ApplicationUser ToUser { get; set; } = null!;
        public Trip? Trip { get; set; }
    }
}
