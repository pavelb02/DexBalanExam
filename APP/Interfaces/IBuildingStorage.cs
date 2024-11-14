using BankSystem.App.Services;
using Domain;

namespace APP.Interfaces;

public interface IBuildingStorage : IStorage<Building, SearchRequest>
{
    Task<Guid> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<Guid> AddSensorAsync(Guid buildingId, Sensor sensor, CancellationToken cancellationToken);
}