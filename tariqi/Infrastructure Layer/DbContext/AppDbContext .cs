using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tariqi.Domain_Layer.Entities;

namespace tariqi.Infrastructure_Layer.DbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Booking -> Payment (One-to-One) [Restrict]
            builder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle -> Driver (SetNull)
            builder.Entity<Vehicle>()
                .HasOne(v => v.Driver)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                 .HasOne(v => v.Area)
                 .WithMany(a => a.Vehicles)
                 .HasForeignKey(v => v.AreaId)
                 .OnDelete(DeleteBehavior.Restrict);


            // Trip -> Creator (Restrict)
            builder.Entity<Trip>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTrips)
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip -> Vehicle (Restrict)
            builder.Entity<Trip>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.Trips)
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip -> Origin Area (Restrict)
            builder.Entity<Trip>()
                .HasOne(t => t.OriginArea)
                .WithMany(a => a.OriginTrips)
                .HasForeignKey(t => t.OriginAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Trip -> Destination Area (Restrict)
            builder.Entity<Trip>()
                .HasOne(t => t.DestinationArea)
                .WithMany(a => a.DestinationTrips)
                .HasForeignKey(t => t.DestinationAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Passenger (Restrict)
            builder.Entity<Booking>()
                .HasOne(b => b.Passenger)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Trip (Restrict)
            builder.Entity<Booking>()
                .HasOne(b => b.Trip)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatMessage -> FromUser (Restrict)
            builder.Entity<ChatMessage>()
                .HasOne(c => c.FromUser)
                .WithMany()
                .HasForeignKey(c => c.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatMessage -> ToUser (Restrict)
            builder.Entity<ChatMessage>()
                .HasOne(c => c.ToUser)
                .WithMany()
                .HasForeignKey(c => c.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatMessage -> Trip (Cascade)
            builder.Entity<ChatMessage>()
                .HasOne(c => c.Trip)
                .WithMany()
                .HasForeignKey(c => c.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Area Precision
            builder.Entity<Area>()
                .Property(a => a.Latitude)
                .HasPrecision(9, 6);

            builder.Entity<Area>()
                .Property(a => a.Longitude)
                .HasPrecision(9, 6);

            // Money Precision
            builder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            builder.Entity<Trip>()
                .Property(t => t.PricePerSeat)
                .HasPrecision(18, 2);

            // Vehicle Plate Number
            builder.Entity<Vehicle>()
                .Property(v => v.PlateNumber)
                .HasMaxLength(7)
                .IsRequired();

           

        }
    }
}
