using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;


public class UserToken : IdentityUserToken<Guid>
{
    public User User { get; init; }
}

internal sealed class UserTokenConfiguration :  IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("user_tokens");

        builder.HasOne(x => x.User);

        builder.HasKey(x => new
        {
            x.LoginProvider,
            x.Name,
            x.UserId,
        });
    }
}