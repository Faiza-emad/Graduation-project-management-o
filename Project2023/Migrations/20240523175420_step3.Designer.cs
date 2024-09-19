﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project2023.Models;

#nullable disable

namespace Project2023.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240523175420_step3")]
    partial class step3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project2023.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeptId")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HOD")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Project2023.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("Project2023.Models.Dept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DeptName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HOD")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("depts");
                });

            modelBuilder.Entity("Project2023.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Closed")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("Decision")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Objective")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Student1")
                        .HasColumnType("int");

                    b.Property<int?>("Student2")
                        .HasColumnType("int");

                    b.Property<int?>("Student3")
                        .HasColumnType("int");

                    b.Property<int?>("Student4")
                        .HasColumnType("int");

                    b.Property<int?>("StudySem")
                        .HasColumnType("int");

                    b.Property<int?>("StudyYear")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkNature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("percentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("Student1");

                    b.HasIndex("Student2");

                    b.HasIndex("Student3");

                    b.HasIndex("Student4");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("Project2023.Models.ProjectAssessment", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorApproved")
                        .HasColumnType("int");

                    b.Property<int?>("PQ1")
                        .HasColumnType("int");

                    b.Property<int?>("PQ2")
                        .HasColumnType("int");

                    b.Property<int?>("PQ3")
                        .HasColumnType("int");

                    b.Property<int?>("PQ4")
                        .HasColumnType("int");

                    b.Property<int?>("PQ5")
                        .HasColumnType("int");

                    b.Property<int?>("PQ6")
                        .HasColumnType("int");

                    b.Property<int?>("PQ7")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.ToTable("ProjectAssessments");
                });

            modelBuilder.Entity("Project2023.Models.ProjectCommDecision", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("AssDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Q1")
                        .HasColumnType("int");

                    b.Property<int?>("Q2")
                        .HasColumnType("int");

                    b.Property<int?>("Q3")
                        .HasColumnType("int");

                    b.Property<int?>("Q4")
                        .HasColumnType("int");

                    b.Property<int?>("Q5")
                        .HasColumnType("int");

                    b.Property<int?>("Q6")
                        .HasColumnType("int");

                    b.Property<int?>("Q7")
                        .HasColumnType("int");

                    b.Property<int?>("Q8")
                        .HasColumnType("int");

                    b.Property<int?>("S1")
                        .HasColumnType("int");

                    b.Property<int?>("S2")
                        .HasColumnType("int");

                    b.Property<int?>("S3")
                        .HasColumnType("int");

                    b.Property<int?>("S4")
                        .HasColumnType("int");

                    b.Property<int?>("S5")
                        .HasColumnType("int");

                    b.Property<int?>("S6")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.ToTable("ProjectCommDecisions");
                });

            modelBuilder.Entity("Project2023.Models.ProjectDissDate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("GradDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Sup1")
                        .HasColumnType("int");

                    b.Property<int?>("Sup2")
                        .HasColumnType("int");

                    b.Property<int?>("Sup3")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("Sup1");

                    b.HasIndex("Sup2");

                    b.HasIndex("Sup3");

                    b.ToTable("ProjectDissDates");
                });

            modelBuilder.Entity("Project2023.Models.ProjectGroup", b =>
                {
                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorGrade")
                        .HasColumnType("int");

                    b.Property<int?>("PQ1")
                        .HasColumnType("int");

                    b.Property<int?>("PQ2")
                        .HasColumnType("int");

                    b.Property<int?>("PQ3")
                        .HasColumnType("int");

                    b.Property<int?>("PQ4")
                        .HasColumnType("int");

                    b.Property<int?>("PQ5")
                        .HasColumnType("int");

                    b.Property<int?>("committeGrade")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectGroups");
                });

            modelBuilder.Entity("Project2023.Models.ProjectWeek", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Date1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date11")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date12")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week11")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week12")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Week9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("percentage1")
                        .HasColumnType("int");

                    b.Property<int?>("percentage10")
                        .HasColumnType("int");

                    b.Property<int?>("percentage11")
                        .HasColumnType("int");

                    b.Property<int?>("percentage12")
                        .HasColumnType("int");

                    b.Property<int?>("percentage2")
                        .HasColumnType("int");

                    b.Property<int?>("percentage3")
                        .HasColumnType("int");

                    b.Property<int?>("percentage4")
                        .HasColumnType("int");

                    b.Property<int?>("percentage5")
                        .HasColumnType("int");

                    b.Property<int?>("percentage6")
                        .HasColumnType("int");

                    b.Property<int?>("percentage7")
                        .HasColumnType("int");

                    b.Property<int?>("percentage8")
                        .HasColumnType("int");

                    b.Property<int?>("percentage9")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.ToTable("ProjectWeeks");
                });

            modelBuilder.Entity("Project2023.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Rate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Project2023.Models.ApplicationUser", b =>
                {
                    b.HasOne("Project2023.Models.Dept", "UserDept")
                        .WithMany()
                        .HasForeignKey("DeptId");

                    b.Navigation("UserDept");
                });

            modelBuilder.Entity("Project2023.Models.Course", b =>
                {
                    b.HasOne("Project2023.Models.Dept", "CourseDept")
                        .WithMany()
                        .HasForeignKey("DeptId");

                    b.Navigation("CourseDept");
                });

            modelBuilder.Entity("Project2023.Models.Project", b =>
                {
                    b.HasOne("Project2023.Models.Course", "ProjectCourse")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("Project2023.Models.ApplicationUser", "ProjectDoctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("Project2023.Models.ApplicationUser", "ProjectStudent1")
                        .WithMany()
                        .HasForeignKey("Student1");

                    b.HasOne("Project2023.Models.ApplicationUser", "ProjectStudent2")
                        .WithMany()
                        .HasForeignKey("Student2");

                    b.HasOne("Project2023.Models.ApplicationUser", "ProjectStudent3")
                        .WithMany()
                        .HasForeignKey("Student3");

                    b.HasOne("Project2023.Models.ApplicationUser", "ProjectStudent4")
                        .WithMany()
                        .HasForeignKey("Student4");

                    b.Navigation("ProjectCourse");

                    b.Navigation("ProjectDoctor");

                    b.Navigation("ProjectStudent1");

                    b.Navigation("ProjectStudent2");

                    b.Navigation("ProjectStudent3");

                    b.Navigation("ProjectStudent4");
                });

            modelBuilder.Entity("Project2023.Models.ProjectAssessment", b =>
                {
                    b.HasOne("Project2023.Models.Project", "Project_ProjectAss")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project_ProjectAss");
                });

            modelBuilder.Entity("Project2023.Models.ProjectCommDecision", b =>
                {
                    b.HasOne("Project2023.Models.Project", "Project_ProjectComm")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project_ProjectComm");
                });

            modelBuilder.Entity("Project2023.Models.ProjectDissDate", b =>
                {
                    b.HasOne("Project2023.Models.Project", "Project_ProjectDiss")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project2023.Models.ApplicationUser", "User_Sup1")
                        .WithMany()
                        .HasForeignKey("Sup1");

                    b.HasOne("Project2023.Models.ApplicationUser", "User_Sup2")
                        .WithMany()
                        .HasForeignKey("Sup2");

                    b.HasOne("Project2023.Models.ApplicationUser", "User_Sup3")
                        .WithMany()
                        .HasForeignKey("Sup3");

                    b.Navigation("Project_ProjectDiss");

                    b.Navigation("User_Sup1");

                    b.Navigation("User_Sup2");

                    b.Navigation("User_Sup3");
                });

            modelBuilder.Entity("Project2023.Models.ProjectGroup", b =>
                {
                    b.HasOne("Project2023.Models.Project", "Project_ProjectGroup")
                        .WithMany("ProjectGroups")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project2023.Models.ApplicationUser", "Project_Student")
                        .WithMany("ProjectStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project_ProjectGroup");

                    b.Navigation("Project_Student");
                });

            modelBuilder.Entity("Project2023.Models.ProjectWeek", b =>
                {
                    b.HasOne("Project2023.Models.Project", "Project_ProjectWeek")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project_ProjectWeek");
                });

            modelBuilder.Entity("Project2023.Models.ApplicationUser", b =>
                {
                    b.Navigation("ProjectStudents");
                });

            modelBuilder.Entity("Project2023.Models.Project", b =>
                {
                    b.Navigation("ProjectGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
