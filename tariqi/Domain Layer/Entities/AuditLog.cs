namespace tariqi.Domain_Layer.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public int EntityId { get; set; }
        public string Action { get; set; } = null!; // Create, Update, Delete
        public string PerformedBy { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Details { get; set; }

        public ApplicationUser PerformedUser { get; set; } = null!;
    }
}
