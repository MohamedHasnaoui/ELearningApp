using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class descriptionAbonnement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Abonnements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Abonnements");
        }
    }
}
