using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ELearningApp.Migrations
{
    /// <inheritdoc />
    public partial class fir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Abonnements",
                columns: new[] { "Id", "Caracteristiques", "Description", "Duree", "IsRecommanded", "Prix", "Type" },
                values: new object[,]
                {
                    { 1, "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing", "Perfect plan for students", 1, false, 199, 0 },
                    { 2, "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing, Access to course community", "For users who want to do more", 1, true, 299, 1 },
                    { 3, "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing, Access to course community", "Your entire friends in one place", 1, false, 499, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Abonnements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Abonnements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Abonnements",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
