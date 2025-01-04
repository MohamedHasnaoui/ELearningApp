using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class dureeAbonneemnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duree",
                table: "Abonnements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Abonnements");
        }
    }
}
