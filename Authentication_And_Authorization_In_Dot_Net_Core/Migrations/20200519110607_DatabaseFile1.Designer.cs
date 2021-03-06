﻿// <auto-generated />
using Authentication_And_Authorization_In_Dot_Net_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200519110607_DatabaseFile1")]
    partial class DatabaseFile1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Authentication_And_Authorization_In_Dot_Net_Core.Models.Boss", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(30)");

                    b.HasKey("Name");

                    b.ToTable("boss");
                });

            modelBuilder.Entity("Authentication_And_Authorization_In_Dot_Net_Core.Models.Employee", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(30)");

                    b.HasKey("Name");

                    b.ToTable("employee");
                });
#pragma warning restore 612, 618
        }
    }
}
