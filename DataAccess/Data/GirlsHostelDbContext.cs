using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class GirlsHostelDbContext : DbContext
    {
        public GirlsHostelDbContext(DbContextOptions<GirlsHostelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Girl> Girls { get; set; }
        public DbSet<RoomHistory> RoomHistories { get; set; }
        public DbSet<FoodAttendance> FoodAttendances { get; set; }
        public DbSet<EntryLog> EntryLogs { get; set; }
        public DbSet<MarketVisit> MarketVisits { get; set; }
        public DbSet<HostelAttendance> HostelAttendances { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<VisitorEntryLog> VisitorEntryLogs { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===============================
            // Hostel → Rooms (RESTRICT)
            // ===============================
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hostel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HostelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.HostelId, r.RoomNumber })
                .IsUnique();

            // ===============================
            // Hostel → Staff (RESTRICT)
            // ===============================
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Hostel)
                .WithMany(h => h.Staffs)
                .HasForeignKey(s => s.HostelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Phone)
                .IsUnique();

            // ===============================
            // Room → Girls (RESTRICT)
            // ===============================
            modelBuilder.Entity<Girl>()
                .HasOne(g => g.Room)
                .WithMany(r => r.Girls)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Girl>()
                .HasIndex(g => g.RoomId);

            // ===============================
            // RoomHistory (RESTRICT)
            // ===============================
            modelBuilder.Entity<RoomHistory>()
                .HasOne(rh => rh.Girl)
                .WithMany(g => g.RoomHistories)
                .HasForeignKey(rh => rh.GirlId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomHistory>()
                .HasOne(rh => rh.Room)
                .WithMany(r => r.RoomHistories)
                .HasForeignKey(rh => rh.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // LOG TABLES (CASCADE ✅)
            // ===============================
            modelBuilder.Entity<FoodAttendance>()
                .HasOne(f => f.Girl)
                .WithMany(g => g.FoodAttendances)
                .HasForeignKey(f => f.GirlId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodAttendance>()
                .HasIndex(f => new { f.GirlId, f.Date, f.MealType })
                .IsUnique();

            modelBuilder.Entity<EntryLog>()
                .HasOne(e => e.Girl)
                .WithMany(g => g.EntryLogs)
                .HasForeignKey(e => e.GirlId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarketVisit>()
                .HasOne(m => m.Girl)
                .WithMany(g => g.MarketVisits)
                .HasForeignKey(m => m.GirlId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HostelAttendance>()
                .HasOne(a => a.Girl)
                .WithMany(g => g.HostelAttendances)
                .HasForeignKey(a => a.GirlId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HostelAttendance>()
                .HasIndex(a => new { a.GirlId, a.Date })
                .IsUnique();

            // ===============================
            // Visitor
            // ===============================
            modelBuilder.Entity<VisitorEntryLog>()
                .HasOne(v => v.Visitor)
                .WithMany(vl => vl.VisitorEntryLogs)
                .HasForeignKey(v => v.VisitorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Visitor>()
                .HasIndex(v => v.Phone);

            modelBuilder.Entity<VisitorEntryLog>()
                .HasOne(v => v.Girl)
                .WithMany(g => g.VisitorEntryLogs)
                .HasForeignKey(v => v.GirlId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===============================
            // 🔥 GLOBAL DEFAULT (IMPORTANT)
            // ===============================
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                if (relationship.DeleteBehavior == DeleteBehavior.Cascade)
                    continue;

                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}