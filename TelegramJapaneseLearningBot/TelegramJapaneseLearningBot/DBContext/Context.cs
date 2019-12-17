using Microsoft.EntityFrameworkCore;
using TelegramJapaneseLearningBot.DBContext.Models;

namespace TelegramJapaneseLearningBot.DBContext
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<LearningUser> Users { get; set; }
        public DbSet<LearningUserSettings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LearningUser>()
                .HasOne(u => u.LearningUserSettings);
        }
    }
}