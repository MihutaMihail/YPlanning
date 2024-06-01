using Microsoft.EntityFrameworkCore;
using YPlanning.Models;

namespace YPlanning.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Member>? Members { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Class>? Classes { get; set; }
        public DbSet<Attendance>? Attendances { get; set; }
        public DbSet<Test>? Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-one relationship between Member and Account
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Account)
                .WithOne(ac => ac.Member)
                .HasForeignKey<Account>(ac => ac.MemberId);

            // One-to-many relationship between Member and Tests
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Tests)
                .WithOne(t => t.Member)
                .HasForeignKey(t => t.MemberId);

            // Many-to-many relationship for Attendaces between Member and Class
            modelBuilder.Entity<Attendance>()
                .HasKey(at => new { at.MemberId, at.ClassId });
            modelBuilder.Entity<Attendance>()
                .HasOne(m => m.Member)
                .WithMany(at => at.Attendances)
                .HasForeignKey(m => m.MemberId);
            modelBuilder.Entity<Attendance>()
                .HasOne(c => c.Class)
                .WithMany(at => at.Attendances)
                .HasForeignKey(c => c.ClassId);
        }
    }
}
