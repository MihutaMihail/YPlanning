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

            // One-to-one relationship between USER and ACCOUNT
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(ac => ac.User)
                .HasForeignKey<Account>(ac => ac.UserId);
            
            // One-to-to relationship between USER and TOKEN
            /*modelBuilder.Entity<User>()
                .HasOne(u => u.)*/

            // One-to-many relationship between USERS and TESTS
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tests)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            // Define primary key for ATTENDANCE
            modelBuilder.Entity<Attendance>()
                .HasKey(at => at.Id);

            // Define foreign key relationships
            modelBuilder.Entity<Attendance>()
                .HasOne(at => at.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Attendance>()
                .HasOne(at => at.Class)
                .WithMany(c => c.Attendances)
                .HasForeignKey(c => c.ClassId);

            // Define primary key for TEST
            modelBuilder.Entity<Test>()
                .HasKey(t => t.Id);

            // Define foreign key relationships
            modelBuilder.Entity<Test>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tests)
                .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<Test>()
                .HasOne(t => t.Class)
                .WithMany(c => c.Tests)
                .HasForeignKey(t => t.ClassId);
        }
    }
}
