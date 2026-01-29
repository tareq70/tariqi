namespace tariqi.Application_Layer.DTOs.Vehicle_DTOs
{
    public class UpdateVehicleDto
    {
        public string? PlateNumber { get; set; }
        public string? Model { get; set; }
        public int? SeatsCount { get; set; }
        public int? AreaId { get; set; }
        public string? DriverId { get; set; }
    }
}
