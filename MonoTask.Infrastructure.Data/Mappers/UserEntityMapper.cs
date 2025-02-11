using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class UserEntityMapper : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired();

        builder.Property(u => u.Roles)
               .HasConversion<int>()
               .IsRequired();

        builder.HasOne(u => u.Token)
               .WithOne(t => t.User)
               .HasForeignKey<UserEntity>(u => u.TokenId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserVehicles)
               .WithOne(uv => uv.User)
               .HasForeignKey(uv => uv.UserId);

        builder.ToTable("Users");
    }
}
