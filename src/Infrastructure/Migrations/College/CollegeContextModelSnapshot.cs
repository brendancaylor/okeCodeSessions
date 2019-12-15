﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CollegeContext))]
    partial class CollegeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Claim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaimName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Claim");
                });

            modelBuilder.Entity("ApplicationCore.Entities.College", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CollegeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByAppUserId");

                    b.HasIndex("UpdatedByAppUserId");

                    b.ToTable("College");
                });

            modelBuilder.Entity("ApplicationCore.Entities.CollegeAppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollegeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CollegeId");

                    b.ToTable("CollegeAppUser");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GoogleSpeechApiRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HomeWorkAssignmentItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SentenceCount")
                        .HasColumnType("int");

                    b.Property<int>("WordCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeWorkAssignmentItemId");

                    b.ToTable("GoogleSpeechApiRequest");
                });

            modelBuilder.Entity("ApplicationCore.Entities.HomeWorkAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("YearClassId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByAppUserId");

                    b.HasIndex("UpdatedByAppUserId");

                    b.HasIndex("YearClassId");

                    b.ToTable("HomeWorkAssignment");
                });

            modelBuilder.Entity("ApplicationCore.Entities.HomeWorkAssignmentItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HomeWorkAssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.Property<string>("Sentence")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("SentenceLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("SpokenSentenceAsMp3")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("SpokenWordAsMp3")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("WordLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByAppUserId");

                    b.HasIndex("HomeWorkAssignmentId");

                    b.HasIndex("UpdatedByAppUserId");

                    b.ToTable("HomeWorkAssignmentItem");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ApplicationCore.Entities.RoleClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SubmittedHomeWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HomeWorkAssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("HomeWorkAssignmentId");

                    b.ToTable("SubmittedHomeWork");
                });

            modelBuilder.Entity("ApplicationCore.Entities.YearClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AcademicYear")
                        .HasColumnType("int");

                    b.Property<Guid>("CollegeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefaultSentenceLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultWordLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedByAppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("YearClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.HasIndex("CreatedByAppUserId");

                    b.HasIndex("UpdatedByAppUserId");

                    b.ToTable("YearClass");
                });

            modelBuilder.Entity("ApplicationCore.Entities.AppUser", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Role", "Role")
                        .WithMany("AppUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.College", b =>
                {
                    b.HasOne("ApplicationCore.Entities.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.AppUser", "UpdatedByAppUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ApplicationCore.Entities.CollegeAppUser", b =>
                {
                    b.HasOne("ApplicationCore.Entities.AppUser", "AppUser")
                        .WithMany("CollegeAppUsers")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.College", "College")
                        .WithMany("CollegeAppUsers")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.GoogleSpeechApiRequest", b =>
                {
                    b.HasOne("ApplicationCore.Entities.HomeWorkAssignmentItem", "HomeWorkAssignmentItem")
                        .WithMany("GoogleSpeechApiRequests")
                        .HasForeignKey("HomeWorkAssignmentItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.HomeWorkAssignment", b =>
                {
                    b.HasOne("ApplicationCore.Entities.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.AppUser", "UpdatedByAppUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ApplicationCore.Entities.YearClass", "YearClass")
                        .WithMany("HomeWorkAssignments")
                        .HasForeignKey("YearClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.HomeWorkAssignmentItem", b =>
                {
                    b.HasOne("ApplicationCore.Entities.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.HomeWorkAssignment", "HomeWorkAssignment")
                        .WithMany("HomeWorkAssignmentItems")
                        .HasForeignKey("HomeWorkAssignmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.AppUser", "UpdatedByAppUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ApplicationCore.Entities.RoleClaim", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Claim", "Claim")
                        .WithMany("RoleClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.SubmittedHomeWork", b =>
                {
                    b.HasOne("ApplicationCore.Entities.HomeWorkAssignment", "HomeWorkAssignment")
                        .WithMany("SubmittedHomeWorks")
                        .HasForeignKey("HomeWorkAssignmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.YearClass", b =>
                {
                    b.HasOne("ApplicationCore.Entities.College", "College")
                        .WithMany("YearClasses")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.AppUser", "UpdatedByAppUser")
                        .WithMany()
                        .HasForeignKey("UpdatedByAppUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
