using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class UserEntityMappings : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired();

        builder.Property(u => u.Roles)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(u => u.Token)
               .IsRequired()
               .HasDefaultValueSql("NEWID()");

        builder.ToTable("Users");
    }
}
