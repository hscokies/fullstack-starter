using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class RoleClaim  : IdentityRoleClaim<Guid>
{
    public Role Role { get; set; }
}

internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("role_claims");

        builder.HasOne(x => x.Role);
    }
}
