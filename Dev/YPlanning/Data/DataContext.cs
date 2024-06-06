using Microsoft.EntityFrameworkCore;
using YPlanning.Models;

namespace YPlanning.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Class>? Classes { get; set; }
        public DbSet<Attendance>? Attendances { get; set; }
        public DbSet<Test>? Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify the table names for all entities
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Account>().ToTable("accounts");
            modelBuilder.Entity<Class>().ToTable("classes");
            modelBuilder.Entity<Attendance>().ToTable("attendances");
            modelBuilder.Entity<Test>().ToTable("tests");

            // One-to-one relationship between User and Account
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(ac => ac.User)
                .HasForeignKey<Account>(ac => ac.UserId);

            // One-to-many relationship between User and Tests
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tests)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            // Many-to-many relationship for Attendaces between User and Class
            modelBuilder.Entity<Attendance>()
                .HasKey(at => new { at.UserId, at.ClassId });
            modelBuilder.Entity<Attendance>()
                .HasOne(u => u.User)
                .WithMany(at => at.Attendances)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Attendance>()
                .HasOne(c => c.Class)
                .WithMany(at => at.Attendances)
                .HasForeignKey(c => c.ClassId);
        }
    }
}
