﻿// <auto-generated />
using System;
using GraphQLTryOuts.Meetings.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQLTryOuts.Meetings.Data.Migrations
{
    [DbContext(typeof(MeetingsDbContext))]
    partial class MeetingsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraphQLTryOuts.Meetings.Data.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("GraphQLTryOuts.Meetings.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GraphQLTryOuts.Meetings.Data.Models.UserMeeting", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MeetingId");

                    b.HasIndex("MeetingId");

                    b.ToTable("UsersMeetings");
                });

            modelBuilder.Entity("GraphQLTryOuts.Meetings.Data.Models.Meeting", b =>
                {
                    b.HasOne("GraphQLTryOuts.Meetings.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("GraphQLTryOuts.Meetings.Data.Models.UserMeeting", b =>
                {
                    b.HasOne("GraphQLTryOuts.Meetings.Data.Models.Meeting", "Meeting")
                        .WithMany("UsersInMeeting")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQLTryOuts.Meetings.Data.Models.User", "User")
                        .WithMany("MeetingsForUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
