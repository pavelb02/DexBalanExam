using BankSystem.App.Dto;
using BankSystem.App.Services;

namespace APP.Interfaces
{
    public interface IBuildingService
    {
        // Получить информацию о здании по его ID
        Task<BuildingDto> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken);

        // Удалить здание
        Task<Guid> DeleteBuildingAsync(Guid buildingId, CancellationToken cancellationToken);

        // Удалить датчик
        Task<Guid> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken);

        // Добавить новое здание
        Task<Guid> AddBuildingAsync(BuildingDto buildingDto, CancellationToken cancellationToken, FileDto? file = null);

        // Добавить датчик в здание
        Task<Guid> AddSensorAsync(Guid buildingId, SensorDto sensorDto, CancellationToken cancellationToken);

        // Обновить данные здания
        Task<Guid> UpdateBuildingAsync(Guid buildingId, BuildingDto newBuilding, CancellationToken cancellationToken);

        // Получить коллекцию зданий по фильтрам
        Task<List<BuildingDto>> FilterBuildingsAsync(SearchRequest searchRequest, CancellationToken cancellationToken);
    }
}