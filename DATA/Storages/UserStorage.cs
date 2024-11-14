using APP.Interfaces;
using BankSystem.App.Services;
using BankSystem.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DATA.Storages;

public class UserStorage : IStorage<User, SearchRequest>
{
    private readonly MyDbContext _dbContext;

    public UserStorage()
    {
        _dbContext = new MyDbContext();
    }
    
    public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Id == user.Id, cancellationToken))
        {
            throw new InvalidOperationException($"Пользователь с ID {user.Id} уже существует.");
        }

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null) throw new ArgumentException($"Пользователь с ID {userId} не найден.");
        
        return user;
    }

    public async Task<List<User>> GetCollectionAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
    {
        IQueryable<User> request = _dbContext.Users;

        if (!string.IsNullOrWhiteSpace(searchRequest.Name))
        {
            request = request.Where(u => u.Name == searchRequest.Name);
        }

        if (searchRequest.PageSize > 0 && searchRequest.PageNumber > 0)
        {
            request = request
                .OrderBy(u => u.Name)
                .Skip((searchRequest.PageNumber - 1) * searchRequest.PageSize)
                .Take(searchRequest.PageSize);
        }

        return await request.ToListAsync(cancellationToken);
    }

    public async Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<Guid> DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null) return Guid.Empty;
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
