using tariqi.Application_Layer.DTOs.GeographicalDtos;
using tariqi.Application_Layer.Exceptions;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Repositories_Interfaces;
using static tariqi.Application_Layer.Services.GeographicalService;

namespace tariqi.Application_Layer.Services
{
    public class GeographicalService : IGeographicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Area> _areaRepo;
        private readonly IRepository<Region> _regionRepo;

        public GeographicalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _areaRepo = _unitOfWork.GetRepository<Area>();
            _regionRepo = _unitOfWork.GetRepository<Region>();
        }

        public async Task<IEnumerable<RegionDto>> GetAllRegionsAsync()
        {
            var regions = await _regionRepo.GetAllAsync();

            return regions.Select(r => new RegionDto(
                Id: r.Id,
                Code: r.Code,
                Name: r.Name,
                AreasCount: r.Areas?.Count ?? 0
            ));
        }
        public async Task<RegionDto> CreateRegionAsync(CreateRegionDto dto)
        {
            var existingRegion = await _regionRepo.FindAsync(r => r.Code == dto.Code || r.Name == dto.Name);

            if (existingRegion.Any())
                throw new DomainValidationException("A region with the same Code or Name already exists.");

            var region = new Region
            {
                Name = dto.Name,
                Code = dto.Code,
            };

            await _regionRepo.AddAsync(region);
            await _unitOfWork.SaveAsync(); 

            return new RegionDto(
                Id: region.Id,
                Name: region.Name,
                Code: region.Code,
                AreasCount: 0
            );
        }
        public async Task<IEnumerable<AreaDto>> FindAreasWithRegionAsync(int regionId)
        {
            var regionExists = await _regionRepo.GetByIdAsync(regionId);

            if(regionExists is null)
                throw new NotFoundException($"Region with Id {regionId} was not found.");

            var areas = await _areaRepo.FindAsync(area => area.RegionId == regionId);

            return areas.Select(a => new AreaDto(
                Id: a.Id,
                RegionId: a.RegionId,
                RegionName: a.Region?.Name ?? string.Empty,
                Name: a.Name,
                Latitude: a.Latitude,
                Longitude: a.Longitude,
                IsActive: a.IsActive,
                OriginTripsCount: a.OriginTrips?.Count ?? 0,
                DestinationTripsCount: a.DestinationTrips?.Count ?? 0
                ));
        }
        public async Task<AreaDto> CreateAreaAsync(CreateAreaDto dto)
        {
          
            var existingArea = await _areaRepo.FindAsync(a => a.RegionId == dto.RegionId && a.Name == dto.Name);

            if (existingArea.Any())
                throw new DomainValidationException($"An area named '{dto.Name}' already exists in this region.");

            var regionExists = await _regionRepo.GetByIdAsync(dto.RegionId);

            if (regionExists is null)
                throw new NotFoundException("The specified region does not exist.");

            var area = new Area
            {
                RegionId = dto.RegionId,
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            };

            await _areaRepo.AddAsync(area);
            await _unitOfWork.SaveAsync();

            return new AreaDto(
                Id: area.Id,
                RegionId: area.RegionId,
                RegionName: area.Region?.Name ?? string.Empty,
                Name: area.Name,
                Latitude: area.Latitude,
                Longitude: area.Longitude,
                IsActive: area.IsActive,
                OriginTripsCount: 0,
                DestinationTripsCount: 0
            );
        }
        public async Task UpdateAreaAsync(int id, UpdateAreaDto dto)
        {
            var areaToUpdate = await _areaRepo.GetByIdAsync(id);

            if (areaToUpdate is null)
                throw new NotFoundException($"Area with Id {id} was not found.");

            if (dto.Name is not null && dto.Name != areaToUpdate.Name)
            {
                var existingArea = await _areaRepo
                    .FindAsync(a => a.RegionId == areaToUpdate.RegionId && a.Name == dto.Name);

                if (existingArea.Any())
                    throw new DomainValidationException($"An area named '{dto.Name}' already exists in this region.");

                areaToUpdate.Name = dto.Name;
            }

            if (dto.Latitude.HasValue)
                areaToUpdate.Latitude = dto.Latitude.Value;

            if (dto.Longitude.HasValue)
                areaToUpdate.Longitude = dto.Longitude.Value;

            if (dto.IsActive.HasValue)
                areaToUpdate.IsActive = dto.IsActive.Value;

            _areaRepo.Update(areaToUpdate);
            await _unitOfWork.SaveAsync();
        }
        public async Task<bool> DeleteAreaAsync(int id)
        {
            var areaToDelete = await _areaRepo.GetByIdAsync(id);

            if (areaToDelete is null)
                throw new NotFoundException($"Area with Id {id} was not found.");

            try
            {
                _areaRepo.Delete(areaToDelete);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new CanNotRemoveException(ex.Message);
            }

        }
    }
}
