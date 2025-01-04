using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class AbonnementAcheteCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbonnementAchetes",
                columns: table => new
                {
                    IdEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdAbonnement = table.Column<int>(type: "int", nullable: false),
                    DateDebutAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonnementAchetes", x => new { x.IdEtudiant, x.IdAbonnement });
                    table.ForeignKey(
                        name: "FK_AbonnementAchetes_Abonnements_IdAbonnement",
                        column: x => x.IdAbonnement,
                        principalTable: "Abonnements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbonnementAchetes_Etudiants_IdEtudiant",
                        column: x => x.IdEtudiant,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementAchetes_IdAbonnement",
                table: "AbonnementAchetes",
                column: "IdAbonnement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbonnementAchetes");
        }
    }
}
