using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class UserTokenMapper : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.AccessToken)
               .IsRequired()
               .HasMaxLength(256);

        builder.Property(t => t.RefreshToken)
               .IsRequired()
               .HasMaxLength(256);

        builder.Property(t => t.AccessTokenExpiry)
               .IsRequired();

        builder.HasOne(t => t.User)
               .WithOne(u => u.Token)
               .HasForeignKey<UserToken>(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.UserId)
           .IsUnique();

        builder.ToTable("UserTokens");
    }
}
