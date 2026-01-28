namespace tariqi.Application_Layer.DTOs.GeographicalDtos
{
    public record UpdateAreaDto(
        int? RegionId,
        string? Name,
        decimal? Latitude,
        decimal? Longitude,
        bool? IsActive
    );
}
