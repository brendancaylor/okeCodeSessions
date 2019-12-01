using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddSubmittedHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubmittedHomeWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    HomeWorkAssignmentId = table.Column<Guid>(nullable: false),
                    StudentName = table.Column<string>(maxLength: 150, nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedHomeWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmittedHomeWork_HomeWorkAssignment_HomeWorkAssignmentId",
                        column: x => x.HomeWorkAssignmentId,
                        principalTable: "HomeWorkAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedHomeWork_HomeWorkAssignmentId",
                table: "SubmittedHomeWork",
                column: "HomeWorkAssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedHomeWork");
        }
    }
}
