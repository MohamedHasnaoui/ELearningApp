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
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_CategoriesCours_CategoryId",
                table: "Cours");

            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Enseignants_EnseignantId",
                table: "Cours");

            migrationBuilder.AlterColumn<float>(
                name: "Evaluation",
                table: "Cours",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "EnseignantId",
                table: "Cours",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cours",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_CategoriesCours_CategoryId",
                table: "Cours",
                column: "CategoryId",
                principalTable: "CategoriesCours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Enseignants_EnseignantId",
                table: "Cours",
                column: "EnseignantId",
                principalTable: "Enseignants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_CategoriesCours_CategoryId",
                table: "Cours");

            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Enseignants_EnseignantId",
                table: "Cours");

            migrationBuilder.AlterColumn<float>(
                name: "Evaluation",
                table: "Cours",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnseignantId",
                table: "Cours",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cours",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_CategoriesCours_CategoryId",
                table: "Cours",
                column: "CategoryId",
                principalTable: "CategoriesCours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Enseignants_EnseignantId",
                table: "Cours",
                column: "EnseignantId",
                principalTable: "Enseignants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
