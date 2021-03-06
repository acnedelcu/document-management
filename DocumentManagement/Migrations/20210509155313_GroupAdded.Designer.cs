// <auto-generated />
using System;
using DocumentManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210509155313_GroupAdded")]
    partial class GroupAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocumentManagement.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DocumentManagement.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyId");

                    b.HasIndex("AddressId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("DocumentManagement.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DocumentManagement.Models.StudyProgram", b =>
                {
                    b.Property<int>("StudyProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DegreeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DistanceLearning")
                        .HasColumnType("bit");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Major")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudyProgramId");

                    b.HasIndex("FacultyId");

                    b.ToTable("StudyPrograms");
                });

            modelBuilder.Entity("DocumentManagement.Models.Faculty", b =>
                {
                    b.HasOne("DocumentManagement.Models.Address", null)
                        .WithMany("Faculties")
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("DocumentManagement.Models.StudyProgram", b =>
                {
                    b.HasOne("DocumentManagement.Models.Faculty", null)
                        .WithMany("StudyPrograms")
                        .HasForeignKey("FacultyId");
                });

            modelBuilder.Entity("DocumentManagement.Models.Address", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("DocumentManagement.Models.Faculty", b =>
                {
                    b.Navigation("StudyPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
