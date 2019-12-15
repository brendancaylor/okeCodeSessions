using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ApiRequestCounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoogleSpeechApiRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HomeWorkAssignmentItemId = table.Column<Guid>(nullable: false),
                    SentenceCount = table.Column<int>(nullable: false),
                    WordCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleSpeechApiRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoogleSpeechApiRequest_HomeWorkAssignmentItem_HomeWorkAssignmentItemId",
                        column: x => x.HomeWorkAssignmentItemId,
                        principalTable: "HomeWorkAssignmentItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoogleSpeechApiRequest_HomeWorkAssignmentItemId",
                table: "GoogleSpeechApiRequest",
                column: "HomeWorkAssignmentItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoogleSpeechApiRequest");
        }
    }
}
