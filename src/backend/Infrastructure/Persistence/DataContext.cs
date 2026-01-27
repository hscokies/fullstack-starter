using Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DataContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin,
    RoleClaim, UserToken>, IDataContext
{
    public DataContext(DbContextOptions<DataContext> dbContext)
        : base(dbContext)
    {
    }

    protected DataContext(DbContextOptions options)
        : base(options)
    {
    }

    public User User { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
