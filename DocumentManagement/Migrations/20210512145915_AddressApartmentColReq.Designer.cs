﻿// <auto-generated />
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
    [Migration("20210512145915_AddressApartmentColReq")]
    partial class AddressApartmentColReq
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

                    b.Property<string>("Apartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
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

            modelBuilder.Entity("DocumentManagement.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DadFirstNameInitial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirtstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecurityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
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

            modelBuilder.Entity("FacultyStudent", b =>
                {
                    b.Property<int>("FacultiesFacultyId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentId")
                        .HasColumnType("int");

                    b.HasKey("FacultiesFacultyId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("FacultyStudent");
                });

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.Property<int>("GroupsGroupId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentId")
                        .HasColumnType("int");

                    b.HasKey("GroupsGroupId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("GroupStudent");
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

            modelBuilder.Entity("FacultyStudent", b =>
                {
                    b.HasOne("DocumentManagement.Models.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultiesFacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentManagement.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.HasOne("DocumentManagement.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentManagement.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
