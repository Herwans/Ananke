﻿// <auto-generated />
using System;
using Ananke.Infrastructure.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ananke.Infrastructure.Migrations
{
    [DbContext(typeof(AnankeContext))]
    [Migration("20240916201159_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ananke.Domain.Entity.Extension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.ExtensionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExtensionTypes");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ExtensionId")
                        .HasColumnType("integer");

                    b.Property<int?>("FolderId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExtensionId");

                    b.HasIndex("FolderId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Extension", b =>
                {
                    b.HasOne("Ananke.Domain.Entity.ExtensionType", "Type")
                        .WithMany("Extensions")
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Folder", b =>
                {
                    b.HasOne("Ananke.Domain.Entity.Folder", "Parent")
                        .WithMany("Folders")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Item", b =>
                {
                    b.HasOne("Ananke.Domain.Entity.Extension", "Extension")
                        .WithMany("Items")
                        .HasForeignKey("ExtensionId");

                    b.HasOne("Ananke.Domain.Entity.Folder", "Folder")
                        .WithMany("Items")
                        .HasForeignKey("FolderId");

                    b.Navigation("Extension");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Extension", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.ExtensionType", b =>
                {
                    b.Navigation("Extensions");
                });

            modelBuilder.Entity("Ananke.Domain.Entity.Folder", b =>
                {
                    b.Navigation("Folders");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
