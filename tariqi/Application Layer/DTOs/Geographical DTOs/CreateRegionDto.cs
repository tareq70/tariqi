using System.ComponentModel.DataAnnotations;

namespace tariqi.Application_Layer.DTOs.GeographicalDtos
{
        public record CreateRegionDto(
            [Required] string Name,
            [Required] string Code
        );
}
