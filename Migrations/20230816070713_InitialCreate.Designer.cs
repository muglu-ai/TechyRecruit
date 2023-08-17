﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TechyRecruit.Migrations
{
    [DbContext(typeof(TechyRecruitContext))]
    [Migration("20230816070713_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("TechyRecruit.Models.RecruitModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CCTC")
                        .HasColumnType("TEXT");

                    b.Property<string>("CandidateName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContractRoleRequired")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ECTC")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExperienceInCloudPlatforms")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExperienceInLeadHandling")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExperienceInPerformanceTesting")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HoldingOfferOrPackageAmount")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NoticePeriodOrLastWorkingDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OpeningDetails")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PreferredLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceivedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recruiter")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RelevantExperience")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TotalExperience")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RecruitModel");
                });
#pragma warning restore 612, 618
        }
    }
}