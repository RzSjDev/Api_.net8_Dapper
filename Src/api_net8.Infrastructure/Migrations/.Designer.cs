﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.net.Data;

#nullable disable

namespace api.net.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240410182850_change_Phone_fieldType_to_long")]
    partial class change_Phone_fieldType_to_long
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("api.net.Models.UserAuth", b =>
                {
                    b.Property<int>("UserAuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAuthId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Passwordhash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.HasKey("UserAuthId");

                    b.ToTable("userAuths");
                });

            modelBuilder.Entity("api.net.Models.UsersContact", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("UserAuthId")
                        .HasColumnType("int");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("phone")
                        .HasColumnType("bigint");

                    b.HasKey("UserId");

                    b.HasIndex("UserAuthId");

                    b.ToTable("usersContacts");
                });

            modelBuilder.Entity("api.net.Models.UsersContact", b =>
                {
                    b.HasOne("api.net.Models.UserAuth", "userAuth")
                        .WithMany("usersContacts")
                        .HasForeignKey("UserAuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userAuth");
                });

            modelBuilder.Entity("api.net.Models.UserAuth", b =>
                {
                    b.Navigation("usersContacts");
                });
#pragma warning restore 612, 618
        }
    }
}
