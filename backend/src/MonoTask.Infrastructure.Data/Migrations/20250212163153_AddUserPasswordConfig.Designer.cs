﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonoTask.Infrastructure.Data.DbContexts;

#nullable disable

namespace MonoTask.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250212163153_AddUserPasswordConfig")]
    partial class AddUserPasswordConfig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<int?>("TokenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TokenId")
                        .IsUnique()
                        .HasFilter("[TokenId] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AccessTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserVehicle", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("UserVehicle", (string)null);
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.VehicleBrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("VehicleBrands", (string)null);
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.VehicleModelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleBrandId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleBrandId");

                    b.ToTable("VehicleModels", (string)null);
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("MonoTask.Infrastructure.Data.Entities.UserToken", "Token")
                        .WithOne("User")
                        .HasForeignKey("MonoTask.Infrastructure.Data.Entities.UserEntity", "TokenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Token");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserVehicle", b =>
                {
                    b.HasOne("MonoTask.Infrastructure.Data.Entities.UserEntity", "User")
                        .WithMany("UserVehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MonoTask.Infrastructure.Data.Entities.VehicleModelEntity", "Vehicle")
                        .WithMany("UserVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.VehicleModelEntity", b =>
                {
                    b.HasOne("MonoTask.Infrastructure.Data.Entities.VehicleBrandEntity", "VehicleBrand")
                        .WithMany("Models")
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("VehicleBrand");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("UserVehicles");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.UserToken", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.VehicleBrandEntity", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("MonoTask.Infrastructure.Data.Entities.VehicleModelEntity", b =>
                {
                    b.Navigation("UserVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
