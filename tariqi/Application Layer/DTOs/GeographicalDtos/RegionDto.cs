namespace tariqi.Application_Layer.DTOs.GeographicalDtos
{
    public record RegionDto(
        int Id,
        string Name,
        string? Code,
        int AreasCount               
    );
}
