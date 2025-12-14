using tariqi.Application_Layer.DTOs.GeographicalDtos;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IGeographicalService
    {
        Task<IEnumerable<RegionDto>> GetAllRegionsAsync();
        Task<RegionDto> CreateRegionAsync(CreateRegionDto dto);
        Task<IEnumerable<AreaDto>> FindAreasWithRegionAsync(int regionId);
        Task<AreaDto> CreateAreaAsync(CreateAreaDto dto);
        Task UpdateAreaAsync(int id, UpdateAreaDto dto);
        Task<bool> DeleteAreaAsync(int id); 
    }
}
