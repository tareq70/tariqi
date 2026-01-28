namespace tariqi.Application_Layer.DTOs.Vehicle_DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int SeatsCount { get; set; }
        public bool IsActive { get; set; }

        public string AreaName { get; set; } = null!;
        public string DriverName { get; set; } = null!;
    }
}
