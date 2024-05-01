using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class CollaboratorTablee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorTablee",
                columns: table => new
                {
                    CollaboratorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaboratorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NotesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTablee", x => x.CollaboratorId);
                    table.ForeignKey(
                        name: "FK_CollaboratorTablee_NotesTablee_NotesId",
                        column: x => x.NotesId,
                        principalTable: "NotesTablee",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollaboratorTablee_UserTablee_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTablee",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTablee_NotesId",
                table: "CollaboratorTablee",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTablee_UserId",
                table: "CollaboratorTablee",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTablee");
        }
    }
}
