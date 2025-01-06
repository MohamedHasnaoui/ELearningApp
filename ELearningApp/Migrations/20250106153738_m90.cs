using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class m90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentaireCorrecteur",
                table: "Soumissions");

            migrationBuilder.DropColumn(
                name: "LienSoumission",
                table: "Soumissions");

            migrationBuilder.AlterColumn<int>(
                name: "Note",
                table: "Soumissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Note",
                table: "Soumissions",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CommentaireCorrecteur",
                table: "Soumissions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LienSoumission",
                table: "Soumissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
