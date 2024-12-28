using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class addedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "CategoriesCours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesCours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Evaluation = table.Column<float>(type: "real", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreateurId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnseignantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cours_AspNetUsers_EnseignantId",
                        column: x => x.EnseignantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cours_CategoriesCours_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoriesCours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignements",
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
                    table.PrimaryKey("PK_Assignements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignements_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateObtention = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LienTelechargement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificats_AspNetUsers_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificats_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursCommences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progression = table.Column<float>(type: "real", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursCommences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursCommences_AspNetUsers_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursCommences_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enonce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Assignements_AssignementId",
                        column: x => x.AssignementId,
                        principalTable: "Assignements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Soumissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignementId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSoumission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LienSoumission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<float>(type: "real", nullable: true),
                    CommentaireCorrecteur = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soumissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soumissions_AspNetUsers_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Soumissions_Assignements_AssignementId",
                        column: x => x.AssignementId,
                        principalTable: "Assignements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texte = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    EstCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choix_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentairesVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentairesVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentairesVideos_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentairesVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReponsesCommentaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentaireId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReponsesCommentaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReponsesCommentaires_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReponsesCommentaires_CommentairesVideos_CommentaireId",
                        column: x => x.CommentaireId,
                        principalTable: "CommentairesVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignements_CoursId",
                table: "Assignements",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_CoursId",
                table: "Certificats",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_EtudiantId",
                table: "Certificats",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Choix_QuestionId",
                table: "Choix",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentairesVideos_UtilisateurId",
                table: "CommentairesVideos",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentairesVideos_VideoId",
                table: "CommentairesVideos",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_CategoryId",
                table: "Cours",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_EnseignantId",
                table: "Cours",
                column: "EnseignantId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursCommences_CoursId",
                table: "CoursCommences",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursCommences_EtudiantId",
                table: "CoursCommences",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AssignementId",
                table: "Questions",
                column: "AssignementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReponsesCommentaires_CommentaireId",
                table: "ReponsesCommentaires",
                column: "CommentaireId");

            migrationBuilder.CreateIndex(
                name: "IX_ReponsesCommentaires_UtilisateurId",
                table: "ReponsesCommentaires",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CoursId",
                table: "Sections",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_Soumissions_AssignementId",
                table: "Soumissions",
                column: "AssignementId");

            migrationBuilder.CreateIndex(
                name: "IX_Soumissions_EtudiantId",
                table: "Soumissions",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SectionId",
                table: "Videos",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Certificats");

            migrationBuilder.DropTable(
                name: "Choix");

            migrationBuilder.DropTable(
                name: "CoursCommences");

            migrationBuilder.DropTable(
                name: "ReponsesCommentaires");

            migrationBuilder.DropTable(
                name: "Soumissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "CommentairesVideos");

            migrationBuilder.DropTable(
                name: "Assignements");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CategoriesCours");
        }
    }
}
