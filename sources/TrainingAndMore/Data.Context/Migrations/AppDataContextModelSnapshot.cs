﻿// <auto-generated />
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Context.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Data.Entities.AppUserCredentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("ExpiresAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FailedLogins")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Credentials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = "2024-10-31",
                            CreatedBy = "System",
                            ExpiresAt = "",
                            FailedLogins = 0,
                            Password = "U3VwZXJTZWNyZXQ2MjhhYzEwYi05NGZlLTQ4MDUtYjE2MS04NTYxOGMzZjVkODc=",
                            RefreshToken = "",
                            Salt = "628ac10b-94fe-4805-b161-85618c3f5d87",
                            UpdatedAt = "2024-10-31",
                            UpdatedBy = "System"
                        });
                });

            modelBuilder.Entity("Data.Entities.AppUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("CredentialsId")
                        .HasColumnType("int");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CredentialsId");

                    b.ToTable("AppUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = "2024-10-31",
                            CreatedBy = "System",
                            CredentialsId = 1,
                            DateOfBirth = "01.01.2010",
                            Email = "test@app.com",
                            FirstName = "",
                            IsActive = true,
                            Name = "",
                            UserRole = 1
                        });
                });

            modelBuilder.Entity("Data.Entities.AppUserEntity", b =>
                {
                    b.HasOne("Data.Entities.AppUserCredentials", "Credentials")
                        .WithMany()
                        .HasForeignKey("CredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credentials");
                });
#pragma warning restore 612, 618
        }
    }
}
