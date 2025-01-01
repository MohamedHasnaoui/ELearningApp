using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.AddColumn<double>(
                name: "Duree",
                table: "Videos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VidPublicId",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Duree",
                table: "Sections",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoursImg",
                table: "Cours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoursImgPublicId",
                table: "Cours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Duree",
                table: "Cours",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VidPublicId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "CoursImg",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "CoursImgPublicId",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Cours");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
