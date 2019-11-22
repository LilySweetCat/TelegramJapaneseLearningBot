using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Migrations
{
    [DbContext(typeof(Context))]
    internal class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TelegramJapaneseLearningBot.Models.User", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId");

                b.ToTable("Users");
            });

            modelBuilder.Entity("TelegramJapaneseLearningBot.Models.UserSettings", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<TimeSpan>("Interval")
                    .HasColumnType("time");

                b.Property<bool>("IsSpeechTraining")
                    .HasColumnType("bit");

                b.Property<bool>("IsTextTraining")
                    .HasColumnType("bit");

                b.HasKey("UserId");

                b.ToTable("UserSettings");
            });

            modelBuilder.Entity("TelegramJapaneseLearningBot.Models.UserSettings", b =>
            {
                b.HasOne("TelegramJapaneseLearningBot.Models.User", "User")
                    .WithOne("UserSettings")
                    .HasForeignKey("TelegramJapaneseLearningBot.Models.UserSettings", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}