using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Models.Task>().ToTable("Tasks");
            modelBuilder.Entity<Subtask>().ToTable("Subtasks");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(File.ReadAllText("key.txt"));
            }
        }
    }
}
