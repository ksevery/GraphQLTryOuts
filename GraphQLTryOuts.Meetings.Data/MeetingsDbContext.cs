using GraphQLTryOuts.Meetings.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLTryOuts.Meetings.Data
{
    public class MeetingsDbContext : DbContext
    {
        public MeetingsDbContext(DbContextOptions<MeetingsDbContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserMeeting> UsersMeetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMeeting>()
                .HasKey(um => new { um.UserId, um.MeetingId });

            modelBuilder.Entity<UserMeeting>()
                .HasOne<User>(um => um.User)
                .WithMany(m => m.MeetingsForUser);

            modelBuilder.Entity<UserMeeting>()
                .HasOne<Meeting>(um => um.Meeting)
                .WithMany(m => m.UsersInMeeting);

            modelBuilder.Entity<Meeting>()
                .HasOne<User>(m => m.Creator)
                .WithMany();
        }
    }
}
