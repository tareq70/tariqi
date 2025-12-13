using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string PassengerId { get; set; } = null!;
        public int SeatsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.PendingPayment;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Trip Trip { get; set; } = null!;
        public ApplicationUser Passenger { get; set; } = null!;
        public Payment? Payment { get; set; }
    }
}
