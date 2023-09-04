﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using catalog_service.Data;

#nullable disable

namespace catalog_service.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("catalog_service.Model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("VatRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"),
                            Name = "Alkohol",
                            VatRate = 19m
                        },
                        new
                        {
                            Id = new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"),
                            Name = "Soft",
                            VatRate = 19m
                        },
                        new
                        {
                            Id = new Guid("708c04fd-4242-4e00-8035-2a966f45f69d"),
                            Name = "Zusatz",
                            VatRate = 7m
                        });
                });

            modelBuilder.Entity("catalog_service.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("984688cc-509d-401b-98e0-fe41efdb397f"),
                            CategoryId = new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"),
                            Description = "Gin",
                            Name = "Gin",
                            Price = 18.5m,
                            Stock = 5
                        },
                        new
                        {
                            Id = new Guid("d5c62730-c3d7-4c5c-9818-ded8318ba7e8"),
                            CategoryId = new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"),
                            Description = "Kostengünstige Alternative zu Jägermeister",
                            Name = "Jagdfürst",
                            Price = 5.99m,
                            Stock = 21
                        },
                        new
                        {
                            Id = new Guid("32d8205f-6340-45f7-9da2-754b9fd4becf"),
                            CategoryId = new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"),
                            Description = "Auch Energon genannt",
                            Name = "Bilig Energy",
                            Price = 0.95m,
                            Stock = 12
                        },
                        new
                        {
                            Id = new Guid("5d495be8-9c44-4680-9352-4f30e4b49f5b"),
                            CategoryId = new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"),
                            Description = "Nektar",
                            Name = "Sauerkirsch",
                            Price = 1.15m,
                            Stock = 25
                        });
                });

            modelBuilder.Entity("catalog_service.Model.Product", b =>
                {
                    b.HasOne("catalog_service.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
