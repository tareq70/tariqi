namespace tariqi.Application_Layer.DTOs.Vehicle_DTOs
{
    public class CreateVehicleDto
    {
        public string PlateNumber { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int SeatsCount { get; set; }
        public int AreaId { get; set; } 
    }
}
