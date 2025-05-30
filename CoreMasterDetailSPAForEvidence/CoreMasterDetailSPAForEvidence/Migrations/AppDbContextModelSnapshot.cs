﻿// <auto-generated />
using System;
using CoreMasterDetailSPAForEvidence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreMasterDetailSPAForEvidence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            CourseName = "C#"
                        },
                        new
                        {
                            CourseId = 2,
                            CourseName = "J2EE"
                        },
                        new
                        {
                            CourseId = 3,
                            CourseName = "NT"
                        });
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.CourseModule", b =>
                {
                    b.Property<int>("CourseModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseModuleId"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("CourseModuleId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseModules");
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnrolled")
                        .HasColumnType("bit");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RegistrationFee")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.CourseModule", b =>
                {
                    b.HasOne("CoreMasterDetailSPAForEvidence.Models.Student", "Student")
                        .WithMany("CourseModules")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.Student", b =>
                {
                    b.HasOne("CoreMasterDetailSPAForEvidence.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.Course", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("CoreMasterDetailSPAForEvidence.Models.Student", b =>
                {
                    b.Navigation("CourseModules");
                });
#pragma warning restore 612, 618
        }
    }
}
