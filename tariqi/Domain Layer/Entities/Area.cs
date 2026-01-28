namespace tariqi.Domain_Layer.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; } = true;

        public Region Region { get; set; } = null!;
        public ICollection<Trip> OriginTrips { get; set; } = new List<Trip>();
        public ICollection<Trip> DestinationTrips { get; set; } = new List<Trip>();
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
