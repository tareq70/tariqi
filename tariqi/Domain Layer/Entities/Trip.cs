using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int OriginRegionId { get; set; }
        public int OriginAreaId { get; set; }
        public int DestinationRegionId { get; set; }
        public int DestinationAreaId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime? EstimatedArrivalTime { get; set; }
        public decimal PricePerSeat { get; set; }
        public TripStatus Status { get; set; } = TripStatus.Scheduled;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Vehicle Vehicle { get; set; } = null!;
        public Area OriginArea { get; set; } = null!;
        public Area DestinationArea { get; set; } = null!;
        public ApplicationUser Creator { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
