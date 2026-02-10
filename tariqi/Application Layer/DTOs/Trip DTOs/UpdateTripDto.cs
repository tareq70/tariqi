using System.ComponentModel.DataAnnotations;

namespace tariqi.Application_Layer.DTOs.Trip_DTOs
{
    public class UpdateTripDto
    {
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? EstimatedArrivalTime { get; set; }

        [Range(1, 10000, ErrorMessage = "Price must be greater than zero")]
        public decimal? PricePerSeat { get; set; }
        public int? OriginAreaId { get; set; }
        public int? DestinationAreaId { get; set; }
    }
}
