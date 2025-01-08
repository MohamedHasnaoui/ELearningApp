using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRaitingAndFollowTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MentorFollows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnseignantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorFollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentorFollows_Enseignants_EnseignantId",
                        column: x => x.EnseignantId,
                        principalTable: "Enseignants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentorFollows_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MentorRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valeur = table.Column<int>(type: "int", nullable: false),
                    EnseignantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentorRatings_Enseignants_EnseignantId",
                        column: x => x.EnseignantId,
                        principalTable: "Enseignants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentorRatings_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentorFollows_EnseignantId",
                table: "MentorFollows",
                column: "EnseignantId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorFollows_EtudiantId",
                table: "MentorFollows",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorRatings_EnseignantId",
                table: "MentorRatings",
                column: "EnseignantId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorRatings_EtudiantId",
                table: "MentorRatings",
                column: "EtudiantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentorFollows");

            migrationBuilder.DropTable(
                name: "MentorRatings");
        }
    }
}
