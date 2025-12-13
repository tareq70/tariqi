using tariqi.Domain_Layer.Enums;

namespace tariqi.Domain_Layer.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Provider { get; set; } = null!; // Paymob, Fawry, Stripe
        public string ProviderReference { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Booking Booking { get; set; } = null!;
    }
}
