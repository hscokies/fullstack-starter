using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class Role : IdentityRole<Guid>;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
    }
}
