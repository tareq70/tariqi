using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.DTOs.Trip_DTOs
{
    public class TripDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public string VehiclePlate { get; set; } = null!;

        public string OriginArea { get; set; } = null!;
        public string DestinationArea { get; set; } = null!;

        public DateTime DepartureDateTime { get; set; }
        public DateTime? EstimatedArrivalTime { get; set; }

        public decimal PricePerSeat { get; set; }

        public int AvailableSeats { get; set; }
        public TripStatus Status { get; set; }
    }
}
