using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEtudiantEnseignantRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnseignantFollowers",
                columns: table => new
                {
                    FollowedByEtudiantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowedEnseignantsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnseignantFollowers", x => new { x.FollowedByEtudiantsId, x.FollowedEnseignantsId });
                    table.ForeignKey(
                        name: "FK_EnseignantFollowers_Enseignants_FollowedEnseignantsId",
                        column: x => x.FollowedEnseignantsId,
                        principalTable: "Enseignants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnseignantFollowers_Etudiants_FollowedByEtudiantsId",
                        column: x => x.FollowedByEtudiantsId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnseignantLikes",
                columns: table => new
                {
                    LikedByEtudiantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedEnseignantsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnseignantLikes", x => new { x.LikedByEtudiantsId, x.LikedEnseignantsId });
                    table.ForeignKey(
                        name: "FK_EnseignantLikes_Enseignants_LikedEnseignantsId",
                        column: x => x.LikedEnseignantsId,
                        principalTable: "Enseignants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnseignantLikes_Etudiants_LikedByEtudiantsId",
                        column: x => x.LikedByEtudiantsId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnseignantFollowers_FollowedEnseignantsId",
                table: "EnseignantFollowers",
                column: "FollowedEnseignantsId");

            migrationBuilder.CreateIndex(
                name: "IX_EnseignantLikes_LikedEnseignantsId",
                table: "EnseignantLikes",
                column: "LikedEnseignantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnseignantFollowers");

            migrationBuilder.DropTable(
                name: "EnseignantLikes");
        }
    }
}
