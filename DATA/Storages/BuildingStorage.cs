using APP.Interfaces;
using BankSystem.App.Services;
using BankSystem.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DATA.Storages
{
    public class BuildingStorage : IBuildingStorage
    {
        private MyDbContext _dbContext;

        public BuildingStorage()
        {
            _dbContext = new MyDbContext();
        }

        public async Task<Guid> AddAsync(Building building, CancellationToken cancellationToken)
        {
            if (await _dbContext.Buildings.AnyAsync(b => b.Id == building.Id))
            {
                throw new InvalidOperationException($"Здание с ID {building.Id} уже существует.");
            }

            await _dbContext.Buildings.AddAsync(building);
            await _dbContext.SaveChangesAsync();

            return building.Id;
        }

        public async Task<Building> GetByIdAsync(Guid buildingId, CancellationToken cancellationToken)
        {
            var building = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId);
            if (building == null)
            {
                throw new ArgumentException($"Здание с Id {buildingId} не найдено.");
            }

            return building;
        }

        public async Task<List<Building>> GetCollectionAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            IQueryable<Building> request = _dbContext.Buildings.Include(b => b.Sensors);
            if (!string.IsNullOrWhiteSpace(searchRequest.Name))
            {
                request = request.Where(b => b.Name == searchRequest.Name);
            }

            if (searchRequest.PageSize != 0 && searchRequest.PageNumber != 0)
            {
                return await request
                    .OrderBy(b => b.Name)
                    .Skip((searchRequest.PageNumber - 1) * searchRequest.PageSize)
                    .Take(searchRequest.PageSize)
                    .ToListAsync();
            }

            return await request.ToListAsync();
        }

        public async Task<Guid> UpdateAsync(Building building, CancellationToken cancellationToken)
        {
            _dbContext.Entry(building).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return building.Id;
        }

        public async Task<Guid> DeleteAsync(Guid buildingId, CancellationToken cancellationToken)
        {
            var building = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId);
            if (building == null)
            {
                return Guid.Empty;
            }

            _dbContext.Buildings.Remove(building);
            await _dbContext.SaveChangesAsync();

            return building.Id;
        }

        public async Task<Guid> AddSensorAsync(Guid buildingId, Sensor sensor, CancellationToken cancellationToken)
        {
            var building = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId);
            if (building == null)
            {
                throw new ArgumentException($"Здание с Id {buildingId} не найдено.");
            }

            building.Sensors.Add(sensor);
            await _dbContext.SaveChangesAsync();

            return building.Id;
        }

        public async Task<Guid> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken)
        {
            var sensor = await _dbContext.Sensors.FirstOrDefaultAsync(s => s.Id == sensorId);
            if (sensor == null)
            {
                throw new ArgumentException($"Датчик с Id {sensorId} не найден.");
            }

            _dbContext.Sensors.Remove(sensor);
            await _dbContext.SaveChangesAsync();

            return sensor.Id;
        }
    }
}
