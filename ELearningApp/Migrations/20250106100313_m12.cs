using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class m12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Duree",
                table: "Sections",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Evaluation",
                table: "Cours",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valeur = table.Column<int>(type: "int", nullable: false),
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CoursId_EtudiantId",
                table: "Ratings",
                columns: new[] { "CoursId", "EtudiantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EtudiantId",
                table: "Ratings",
                column: "EtudiantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.AlterColumn<double>(
                name: "Duree",
                table: "Sections",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Evaluation",
                table: "Cours",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
