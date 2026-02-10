using System.ComponentModel.DataAnnotations;

namespace tariqi.Application_Layer.DTOs.Trip_DTOs
{
    public class CreateTripDto
    {
        [Required(ErrorMessage = "Vehicle is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid vehicle id")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Origin area is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid origin area id")]
        public int OriginAreaId { get; set; }

        [Required(ErrorMessage = "Destination area is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid destination area id")]
        public int DestinationAreaId { get; set; }

        [Required(ErrorMessage = "Departure date is required")]
        public DateTime DepartureDateTime { get; set; }

        // Optional
        public DateTime? EstimatedArrivalTime { get; set; }

        [Required(ErrorMessage = "Price per seat is required")]
        [Range(1, 10000, ErrorMessage = "Price must be greater than zero")]
        public decimal PricePerSeat { get; set; }
    }
}
