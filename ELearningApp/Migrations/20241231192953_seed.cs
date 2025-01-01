using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoriesCours",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Courses related to programming languages and software development.", "Programming" },
                    { 2, "Courses related to data analysis, machine learning, and statistics.", "Data Science" },
                    { 3, "Courses related to building websites and web applications.", "Web Development" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoriesCours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoriesCours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoriesCours",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
