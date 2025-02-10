using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class VehicleBrandEntityMapper : IEntityTypeConfiguration<VehicleBrandEntity>
{
    public void Configure(EntityTypeBuilder<VehicleBrandEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Country)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired();

        builder.HasMany(c => c.Models)
            .WithOne(vm => vm.VehicleBrand)
            .HasForeignKey(vm => vm.VehicleBrandId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("VehicleBrands");
    }
}