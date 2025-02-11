using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class VehicleModelEntityMapper : IEntityTypeConfiguration<VehicleModelEntity>
{
    public void Configure(EntityTypeBuilder<VehicleModelEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Year)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired();

        builder.HasOne(c => c.VehicleBrand)
            .WithMany(vb => vb.Models)
            .HasForeignKey(c => c.VehicleBrandId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(vm => vm.UserVehicles)
            .WithOne(uv => uv.Vehicle)
            .HasForeignKey(uv => uv.VehicleId);

        builder.ToTable("VehicleModels");
    }
}
