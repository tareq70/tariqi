namespace tariqi.Application_Layer.DTOs.Vehicle_DTOs
{
    public class UpdateVehicleDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int SeatsCount { get; set; }
        public bool IsActive { get; set; } 
    }
}
