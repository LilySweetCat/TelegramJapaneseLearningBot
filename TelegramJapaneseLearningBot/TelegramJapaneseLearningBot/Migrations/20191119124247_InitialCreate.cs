using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramJapaneseLearningBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    UserId = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.UserId); });

            migrationBuilder.CreateTable(
                "UserSettings",
                table => new
                {
                    UserId = table.Column<string>(),
                    IsSpeechTraining = table.Column<bool>(),
                    IsTextTraining = table.Column<bool>(),
                    Interval = table.Column<TimeSpan>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.UserId);
                    table.ForeignKey(
                        "FK_UserSettings_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserSettings");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}