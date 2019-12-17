﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TelegramJapaneseLearningBot.DBContext.Models.LearningUser", b =>
                {
                    b.Property<int>("LearningUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LearningUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TelegramJapaneseLearningBot.DBContext.Models.LearningUserSettings", b =>
                {
                    b.Property<int>("LearningUserId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Interval")
                        .HasColumnType("time");

                    b.Property<bool>("IsSpeechTraining")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextTraining")
                        .HasColumnType("bit");

                    b.HasKey("LearningUserId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("TelegramJapaneseLearningBot.DBContext.Models.LearningUserSettings", b =>
                {
                    b.HasOne("TelegramJapaneseLearningBot.DBContext.Models.LearningUser", "LearningUser")
                        .WithOne("LearningUserSettings")
                        .HasForeignKey("TelegramJapaneseLearningBot.DBContext.Models.LearningUserSettings", "LearningUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
