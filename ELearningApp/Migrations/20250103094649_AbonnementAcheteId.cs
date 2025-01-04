using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class AbonnementAcheteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbonnementAchetes",
                table: "AbonnementAchetes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AbonnementAchetes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbonnementAchetes",
                table: "AbonnementAchetes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementAchetes_IdEtudiant",
                table: "AbonnementAchetes",
                column: "IdEtudiant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbonnementAchetes",
                table: "AbonnementAchetes");

            migrationBuilder.DropIndex(
                name: "IX_AbonnementAchetes_IdEtudiant",
                table: "AbonnementAchetes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AbonnementAchetes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbonnementAchetes",
                table: "AbonnementAchetes",
                columns: new[] { "IdEtudiant", "IdAbonnement" });
        }
    }
}
