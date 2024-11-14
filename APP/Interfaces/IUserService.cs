using BankSystem.App.Dto;
using BankSystem.App.Services;

namespace APP.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<Guid> AddUserAsync(UserDto userDto, CancellationToken cancellationToken);
    Task<Guid> UpdateUserAsync(Guid userId, UserDto userDto, CancellationToken cancellationToken);
    Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<UserDto>> FilterUsersAsync(SearchRequest searchRequest, CancellationToken cancellationToken);
        
    // Методы для работы с зданиями и датчиками
    Task<Guid> AddBuildingToUserAsync(Guid userId, BuildingDto buildingDto, CancellationToken cancellationToken);
    Task<Guid> AddSensorToBuildingAsync(Guid buildingId, SensorDto sensorDto, CancellationToken cancellationToken);
}
