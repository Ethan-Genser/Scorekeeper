using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scorekeeper.Data.Migrations
{
    /// <inheritdoc />
    public partial class initializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scoreboards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoreboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserScoreboard",
                columns: table => new
                {
                    ScoreboardsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserScoreboard", x => new { x.ScoreboardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserScoreboard_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserScoreboard_Scoreboards_ScoreboardsId",
                        column: x => x.ScoreboardsId,
                        principalTable: "Scoreboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserScoreboard_UsersId",
                table: "ApplicationUserScoreboard",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserScoreboard");

            migrationBuilder.DropTable(
                name: "Scoreboards");
        }
    }
}
