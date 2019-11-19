using Microsoft.EntityFrameworkCore;
using TelegramJapaneseLearningBot.Models;

namespace TelegramJapaneseLearningBot.DBContext
{
    public sealed class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(b => b.UserId)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasKey(c => c.UserId);
        }
    }
}
