using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class UserLogin : IdentityUserLogin<Guid>
{
    public User User { get; init; }
}

internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("user_login");

        builder.HasOne(x => x.User);
        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });
    }
}
