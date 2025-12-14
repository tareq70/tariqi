namespace tariqi.Domain_Layer.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int SeatsCount { get; set; }
        public string? DriverId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser? Driver { get; set; }
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
