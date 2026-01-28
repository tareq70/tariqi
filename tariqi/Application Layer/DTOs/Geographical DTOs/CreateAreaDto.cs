using System.ComponentModel.DataAnnotations;
namespace tariqi.Application_Layer.DTOs.GeographicalDtos
{

    public record CreateAreaDto(
        [Required(ErrorMessage = "Region ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Region ID must be a valid number.")]
        int RegionId,

        [Required(ErrorMessage = "Area name is required.")]
        [StringLength(100, MinimumLength = 2)]
        string Name,

        decimal? Latitude,
        decimal? Longitude
    );
}
