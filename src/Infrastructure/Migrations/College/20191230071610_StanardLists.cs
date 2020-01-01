using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class StanardLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionLevel",
                table: "College",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StandardList",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StandardListName = table.Column<string>(nullable: true),
                    DefaultWordLanguage = table.Column<string>(nullable: true),
                    DefaultSentenceLanguage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StandardListItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StandardListId = table.Column<Guid>(nullable: false),
                    Sentence = table.Column<string>(nullable: true),
                    Word = table.Column<string>(nullable: true),
                    SpokenWordAsMp3 = table.Column<byte[]>(nullable: true),
                    SpokenSentenceAsMp3 = table.Column<byte[]>(nullable: true),
                    WordLanguage = table.Column<string>(nullable: true),
                    SentenceLanguage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardListItem_StandardList_StandardListId",
                        column: x => x.StandardListId,
                        principalTable: "StandardList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StandardListItem_StandardListId",
                table: "StandardListItem",
                column: "StandardListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandardListItem");

            migrationBuilder.DropTable(
                name: "StandardList");

            migrationBuilder.DropColumn(
                name: "SubscriptionLevel",
                table: "College");
        }
    }
}
