using APP.Interfaces;
using AutoMapper;
using BankSystem.App.Dto;
using BankSystem.App.Services;
using Domain;

namespace APP.Services
{
    public class UserService : IUserService
    {
        private readonly IStorage<User, SearchRequest> _userStorage;
        private readonly IStorage<Building, SearchRequest> _buildingStorage;
        private readonly IStorage<Sensor, SearchRequest> _sensorStorage;
        private readonly IMapper _mapper;

        public UserService(
            IStorage<User, SearchRequest> userStorage,
            IStorage<Building, SearchRequest> buildingStorage,
            IStorage<Sensor, SearchRequest> sensorStorage,
            IMapper mapper)
        {
            _userStorage = userStorage;
            _buildingStorage = buildingStorage;
            _sensorStorage = sensorStorage;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userStorage.GetByIdAsync(userId, cancellationToken);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<Guid> AddUserAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(userDto);
            var userId = await _userStorage.AddAsync(user, cancellationToken);
            return userId;
        }

        public async Task<Guid> UpdateUserAsync(Guid userId, UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userStorage.GetByIdAsync(userId, cancellationToken);
            _mapper.Map(userDto, user);
            var updatedUserId = await _userStorage.UpdateAsync(user, cancellationToken);
            return updatedUserId;
        }

        public async Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var deletedUserId = await _userStorage.DeleteAsync(userId, cancellationToken);
            return deletedUserId;
        }

        public async Task<List<UserDto>> FilterUsersAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            var users = await _userStorage.GetCollectionAsync(searchRequest, cancellationToken);
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<Guid> AddBuildingToUserAsync(Guid userId, BuildingDto buildingDto, CancellationToken cancellationToken)
        {
            var building = _mapper.Map<Building>(buildingDto);
            building.UserId = userId; // Привязываем здание к пользователю
            var buildingId = await _buildingStorage.AddAsync(building, cancellationToken);
            return buildingId;
        }

        public async Task<Guid> AddSensorToBuildingAsync(Guid buildingId, SensorDto sensorDto, CancellationToken cancellationToken)
        {
            var sensor = _mapper.Map<Sensor>(sensorDto);
            sensor.BuildingId = buildingId; // Привязываем датчик к зданию
            var sensorId = await _sensorStorage.AddAsync(sensor, cancellationToken);
            return sensorId;
        }
    }
}
