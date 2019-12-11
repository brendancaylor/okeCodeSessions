using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangesToHwItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SubmittedHomeWork");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SubmittedHomeWork");

            migrationBuilder.AddColumn<byte[]>(
                name: "SpokenSentenceAsMp3",
                table: "HomeWorkAssignmentItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpokenSentenceAsMp3",
                table: "HomeWorkAssignmentItem");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SubmittedHomeWork",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "SubmittedHomeWork",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
