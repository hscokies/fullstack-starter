using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Persistence;

public interface IReadOnlyDataContext
{
    DatabaseFacade Database { get; }
    
    User User { get; init; }
}

public interface IDataContext : IReadOnlyDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
