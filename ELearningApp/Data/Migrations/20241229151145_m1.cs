using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentairesVideos_AspNetUsers_UtilisateurId",
                table: "CommentairesVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentairesVideos_Videos_VideoId",
                table: "CommentairesVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Assignements_AssignementId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReponsesCommentaires_AspNetUsers_UtilisateurId",
                table: "ReponsesCommentaires");

            migrationBuilder.DropForeignKey(
                name: "FK_ReponsesCommentaires_CommentairesVideos_CommentaireId",
                table: "ReponsesCommentaires");

            migrationBuilder.DropForeignKey(
                name: "FK_Soumissions_Assignements_AssignementId",
                table: "Soumissions");

            migrationBuilder.DropTable(
                name: "Assignements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReponsesCommentaires",
                table: "ReponsesCommentaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentairesVideos",
                table: "CommentairesVideos");

            migrationBuilder.RenameTable(
                name: "ReponsesCommentaires",
                newName: "ReponsesCommentaire");

            migrationBuilder.RenameTable(
                name: "CommentairesVideos",
                newName: "CommentairesVideo");

            migrationBuilder.RenameColumn(
                name: "AssignementId",
                table: "Soumissions",
                newName: "ExamenId");

            migrationBuilder.RenameIndex(
                name: "IX_Soumissions_AssignementId",
                table: "Soumissions",
                newName: "IX_Soumissions_ExamenId");

            migrationBuilder.RenameColumn(
                name: "AssignementId",
                table: "Questions",
                newName: "ExamenId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_AssignementId",
                table: "Questions",
                newName: "IX_Questions_ExamenId");

            migrationBuilder.RenameIndex(
                name: "IX_ReponsesCommentaires_UtilisateurId",
                table: "ReponsesCommentaire",
                newName: "IX_ReponsesCommentaire_UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_ReponsesCommentaires_CommentaireId",
                table: "ReponsesCommentaire",
                newName: "IX_ReponsesCommentaire_CommentaireId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentairesVideos_VideoId",
                table: "CommentairesVideo",
                newName: "IX_CommentairesVideo_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentairesVideos_UtilisateurId",
                table: "CommentairesVideo",
                newName: "IX_CommentairesVideo_UtilisateurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReponsesCommentaire",
                table: "ReponsesCommentaire",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentairesVideo",
                table: "CommentairesVideo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Examens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examens_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examens_CoursId",
                table: "Examens",
                column: "CoursId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentairesVideo_AspNetUsers_UtilisateurId",
                table: "CommentairesVideo",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentairesVideo_Videos_VideoId",
                table: "CommentairesVideo",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Examens_ExamenId",
                table: "Questions",
                column: "ExamenId",
                principalTable: "Examens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReponsesCommentaire_AspNetUsers_UtilisateurId",
                table: "ReponsesCommentaire",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReponsesCommentaire_CommentairesVideo_CommentaireId",
                table: "ReponsesCommentaire",
                column: "CommentaireId",
                principalTable: "CommentairesVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Soumissions_Examens_ExamenId",
                table: "Soumissions",
                column: "ExamenId",
                principalTable: "Examens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentairesVideo_AspNetUsers_UtilisateurId",
                table: "CommentairesVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentairesVideo_Videos_VideoId",
                table: "CommentairesVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Examens_ExamenId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReponsesCommentaire_AspNetUsers_UtilisateurId",
                table: "ReponsesCommentaire");

            migrationBuilder.DropForeignKey(
                name: "FK_ReponsesCommentaire_CommentairesVideo_CommentaireId",
                table: "ReponsesCommentaire");

            migrationBuilder.DropForeignKey(
                name: "FK_Soumissions_Examens_ExamenId",
                table: "Soumissions");

            migrationBuilder.DropTable(
                name: "Examens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReponsesCommentaire",
                table: "ReponsesCommentaire");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentairesVideo",
                table: "CommentairesVideo");

            migrationBuilder.RenameTable(
                name: "ReponsesCommentaire",
                newName: "ReponsesCommentaires");

            migrationBuilder.RenameTable(
                name: "CommentairesVideo",
                newName: "CommentairesVideos");

            migrationBuilder.RenameColumn(
                name: "ExamenId",
                table: "Soumissions",
                newName: "AssignementId");

            migrationBuilder.RenameIndex(
                name: "IX_Soumissions_ExamenId",
                table: "Soumissions",
                newName: "IX_Soumissions_AssignementId");

            migrationBuilder.RenameColumn(
                name: "ExamenId",
                table: "Questions",
                newName: "AssignementId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ExamenId",
                table: "Questions",
                newName: "IX_Questions_AssignementId");

            migrationBuilder.RenameIndex(
                name: "IX_ReponsesCommentaire_UtilisateurId",
                table: "ReponsesCommentaires",
                newName: "IX_ReponsesCommentaires_UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_ReponsesCommentaire_CommentaireId",
                table: "ReponsesCommentaires",
                newName: "IX_ReponsesCommentaires_CommentaireId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentairesVideo_VideoId",
                table: "CommentairesVideos",
                newName: "IX_CommentairesVideos_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentairesVideo_UtilisateurId",
                table: "CommentairesVideos",
                newName: "IX_CommentairesVideos_UtilisateurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReponsesCommentaires",
                table: "ReponsesCommentaires",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentairesVideos",
                table: "CommentairesVideos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Assignements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignements_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignements_CoursId",
                table: "Assignements",
                column: "CoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentairesVideos_AspNetUsers_UtilisateurId",
                table: "CommentairesVideos",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentairesVideos_Videos_VideoId",
                table: "CommentairesVideos",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Assignements_AssignementId",
                table: "Questions",
                column: "AssignementId",
                principalTable: "Assignements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReponsesCommentaires_AspNetUsers_UtilisateurId",
                table: "ReponsesCommentaires",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReponsesCommentaires_CommentairesVideos_CommentaireId",
                table: "ReponsesCommentaires",
                column: "CommentaireId",
                principalTable: "CommentairesVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Soumissions_Assignements_AssignementId",
                table: "Soumissions",
                column: "AssignementId",
                principalTable: "Assignements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
