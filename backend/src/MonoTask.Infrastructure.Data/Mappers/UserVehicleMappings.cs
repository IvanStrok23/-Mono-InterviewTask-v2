using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Mappers;

public class UserVehicleMapper : IEntityTypeConfiguration<UserVehicle>
{
    public void Configure(EntityTypeBuilder<UserVehicle> builder)
    {
        builder.HasKey(uv => new { uv.UserId, uv.VehicleId });

        builder.HasOne(uv => uv.User)
               .WithMany(u => u.UserVehicles)
               .HasForeignKey(uv => uv.UserId);

        builder.HasOne(uv => uv.Vehicle)
               .WithMany(v => v.UserVehicles)
               .HasForeignKey(uv => uv.VehicleId);


        builder.ToTable("UserVehicle");
    }
}
