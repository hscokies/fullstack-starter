using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public User User { get; init; }
    public Role Role { get; init; }
}

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_role");

        builder.HasOne(x => x.Role);
        builder.HasOne(x => x.User);
        
        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}
