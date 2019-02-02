using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RaceTracker.Data.Models;

namespace RaceTracker.Data
{
    public class RaceTrackerContext : DbContext
    {
        public RaceTrackerContext() {}
        public RaceTrackerContext(DbContextOptions<RaceTrackerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Leader>()
                .Property(b => b.Checkins)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, DateTime>>(v));
        }

        public DbSet<RaceEvent> RaceEvents { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<Watcher> Watchers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Leader> Leaders { get; set; }
    }
}
