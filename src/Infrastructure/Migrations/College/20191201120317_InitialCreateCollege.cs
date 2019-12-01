using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreateCollege : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClaimName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IdentityId = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    FirstName = table.Column<string>(maxLength: 250, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "College",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreatedByAppUserId = table.Column<Guid>(nullable: false),
                    UpdatedByAppUserId = table.Column<Guid>(nullable: true),
                    CollegeName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_College", x => x.Id);
                    table.ForeignKey(
                        name: "FK_College_AppUser_CreatedByAppUserId",
                        column: x => x.CreatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_College_AppUser_UpdatedByAppUserId",
                        column: x => x.UpdatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollegeAppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CollegeId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeAppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollegeAppUser_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollegeAppUser_College_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "College",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearClass",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreatedByAppUserId = table.Column<Guid>(nullable: false),
                    UpdatedByAppUserId = table.Column<Guid>(nullable: true),
                    CollegeId = table.Column<Guid>(nullable: false),
                    AcademicYear = table.Column<int>(nullable: false),
                    YearClassName = table.Column<string>(maxLength: 50, nullable: false),
                    TeacherName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearClass_College_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "College",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearClass_AppUser_CreatedByAppUserId",
                        column: x => x.CreatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearClass_AppUser_UpdatedByAppUserId",
                        column: x => x.UpdatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeWorkAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreatedByAppUserId = table.Column<Guid>(nullable: false),
                    UpdatedByAppUserId = table.Column<Guid>(nullable: true),
                    YearClassId = table.Column<Guid>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeWorkAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignment_AppUser_CreatedByAppUserId",
                        column: x => x.CreatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignment_AppUser_UpdatedByAppUserId",
                        column: x => x.UpdatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignment_YearClass_YearClassId",
                        column: x => x.YearClassId,
                        principalTable: "YearClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeWorkAssignmentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreatedByAppUserId = table.Column<Guid>(nullable: false),
                    UpdatedByAppUserId = table.Column<Guid>(nullable: true),
                    HomeWorkAssignmentId = table.Column<Guid>(nullable: false),
                    Sentence = table.Column<string>(maxLength: 1000, nullable: false),
                    Word = table.Column<string>(maxLength: 50, nullable: false),
                    SpokenWordAsMp3 = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeWorkAssignmentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignmentItem_AppUser_CreatedByAppUserId",
                        column: x => x.CreatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignmentItem_HomeWorkAssignment_HomeWorkAssignmentId",
                        column: x => x.HomeWorkAssignmentId,
                        principalTable: "HomeWorkAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeWorkAssignmentItem_AppUser_UpdatedByAppUserId",
                        column: x => x.UpdatedByAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_RoleId",
                table: "AppUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_College_CreatedByAppUserId",
                table: "College",
                column: "CreatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_College_UpdatedByAppUserId",
                table: "College",
                column: "UpdatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeAppUser_AppUserId",
                table: "CollegeAppUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeAppUser_CollegeId",
                table: "CollegeAppUser",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignment_CreatedByAppUserId",
                table: "HomeWorkAssignment",
                column: "CreatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignment_UpdatedByAppUserId",
                table: "HomeWorkAssignment",
                column: "UpdatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignment_YearClassId",
                table: "HomeWorkAssignment",
                column: "YearClassId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignmentItem_CreatedByAppUserId",
                table: "HomeWorkAssignmentItem",
                column: "CreatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignmentItem_HomeWorkAssignmentId",
                table: "HomeWorkAssignmentItem",
                column: "HomeWorkAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorkAssignmentItem_UpdatedByAppUserId",
                table: "HomeWorkAssignmentItem",
                column: "UpdatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_ClaimId",
                table: "RoleClaim",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_YearClass_CollegeId",
                table: "YearClass",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_YearClass_CreatedByAppUserId",
                table: "YearClass",
                column: "CreatedByAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_YearClass_UpdatedByAppUserId",
                table: "YearClass",
                column: "UpdatedByAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeAppUser");

            migrationBuilder.DropTable(
                name: "HomeWorkAssignmentItem");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "HomeWorkAssignment");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "YearClass");

            migrationBuilder.DropTable(
                name: "College");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
