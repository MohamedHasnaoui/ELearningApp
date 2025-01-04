using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class recommandedAbonnement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecommanded",
                table: "Abonnements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecommanded",
                table: "Abonnements");
        }
    }
}
