using APP.Interfaces;
using AutoMapper;
using BankSystem.App.Dto;
using BankSystem.App.Interfaces;
using BankSystem.App.Services;
using Domain;

namespace APP.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingStorage _buildingStorage;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingStorage buildingStorage, IFileService fileService, IMapper mapper)
        {
            _buildingStorage = buildingStorage;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<BuildingDto> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken)
        {
            var building = await _buildingStorage.GetByIdAsync(buildingId, cancellationToken);
            var buildingDto = _mapper.Map<BuildingDto>(building);
            return buildingDto;
        }

        public async Task<Guid> DeleteBuildingAsync(Guid buildingId, CancellationToken cancellationToken)
        {
            var deletedId = await _buildingStorage.DeleteAsync(buildingId, cancellationToken);
            return deletedId;
        }

        public async Task<Guid> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken)
        {
            var deletedSensorId = await _buildingStorage.DeleteSensorAsync(sensorId, cancellationToken);
            return deletedSensorId;
        }

        public async Task<Guid> AddBuildingAsync(BuildingDto buildingDto, CancellationToken cancellationToken, FileDto? file = null)
        {
            try
            {
                if (!await ValidateAddBuildingAsync(buildingDto))
                {
                    return Guid.Empty;
                }

                if (file != null)
                {
                    buildingDto.ImagePath = await _fileService.SaveFile(file);
                }
                else
                {
                    buildingDto.ImagePath = string.Empty;
                }

                var building = _mapper.Map<Building>(buildingDto);
                var buildingId = await _buildingStorage.AddAsync(building, cancellationToken);
                return buildingId;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Ошибка при добавлении здания: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\nТрассировка стека: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Guid> AddSensorAsync(Guid buildingId, SensorDto sensorDto, CancellationToken cancellationToken)
        {
            try
            {
                var sensor = _mapper.Map<Sensor>(sensorDto);
                var sensorId = await _buildingStorage.AddSensorAsync(buildingId, sensor, cancellationToken);
                return sensorId;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Ошибка при добавлении датчика: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\nТрассировка стека: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Guid> UpdateBuildingAsync(Guid buildingId, BuildingDto newBuilding, CancellationToken cancellationToken)
        {
            try
            {
                var updatedBuildingId = Guid.Empty;
                if (await ValidateAddBuildingAsync(newBuilding))
                {
                    var building = await _buildingStorage.GetByIdAsync(buildingId, cancellationToken);

                    if (!string.IsNullOrWhiteSpace(newBuilding.Name))
                        building.Name = newBuilding.Name;


                    updatedBuildingId = await _buildingStorage.UpdateAsync(building, cancellationToken);
                }
                return updatedBuildingId;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Ошибка при обновлении здания: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\nТрассировка стека: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<BuildingDto>> FilterBuildingsAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            var filteredBuildings = await _buildingStorage.GetCollectionAsync(searchRequest, cancellationToken);
            var filteredBuildingsDto = _mapper.Map<List<BuildingDto>>(filteredBuildings);
            return filteredBuildingsDto;
        }

        private static Task<bool> ValidateAddBuildingAsync(BuildingDto building)
        {
            if (string.IsNullOrWhiteSpace(building.Name))
            {
                throw new ArgumentException("Название не может быть null, пустым или состоять только из пробелов.", nameof(building.Name));
            }

           
            return Task.FromResult(true);
        }
    }
}
