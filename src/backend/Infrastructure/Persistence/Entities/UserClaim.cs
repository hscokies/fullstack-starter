using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class UserClaim : IdentityUserClaim<Guid>
{
    public User User { get; init; }
}

internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("user_claim");
        
        builder.HasOne(x => x.User);
    }
}