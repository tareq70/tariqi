namespace tariqi.Application_Layer.DTOs.GeographicalDtos
{
    public record AreaDto(
        int Id,
        int RegionId,
        string RegionName,        
        string Name,
        decimal? Latitude,
        decimal? Longitude,
        bool IsActive,
        int OriginTripsCount,    
        int DestinationTripsCount     
    );
}
