using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class LanguageCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultSentenceLanguage",
                table: "YearClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultWordLanguage",
                table: "YearClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentenceLanguage",
                table: "HomeWorkAssignmentItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WordLanguage",
                table: "HomeWorkAssignmentItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSentenceLanguage",
                table: "YearClass");

            migrationBuilder.DropColumn(
                name: "DefaultWordLanguage",
                table: "YearClass");

            migrationBuilder.DropColumn(
                name: "SentenceLanguage",
                table: "HomeWorkAssignmentItem");

            migrationBuilder.DropColumn(
                name: "WordLanguage",
                table: "HomeWorkAssignmentItem");
        }
    }
}
