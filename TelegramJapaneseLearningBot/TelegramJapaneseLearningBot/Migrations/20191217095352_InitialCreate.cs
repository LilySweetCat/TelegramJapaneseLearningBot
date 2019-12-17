using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramJapaneseLearningBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    LearningUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LearningUserId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    LearningUserId = table.Column<int>(nullable: false),
                    IsSpeechTraining = table.Column<bool>(nullable: false),
                    IsTextTraining = table.Column<bool>(nullable: false),
                    Interval = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.LearningUserId);
                    table.ForeignKey(
                        name: "FK_Settings_Users_LearningUserId",
                        column: x => x.LearningUserId,
                        principalTable: "Users",
                        principalColumn: "LearningUserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
