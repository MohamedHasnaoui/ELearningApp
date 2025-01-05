using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progression",
                table: "CoursCommences");

            migrationBuilder.AddColumn<int>(
                name: "Progres",
                table: "CoursCommences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progres",
                table: "CoursCommences");

            migrationBuilder.AddColumn<float>(
                name: "Progression",
                table: "CoursCommences",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
