using ELearningApp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ELearningApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Cours> Cours { get; set; }
        public DbSet<CategoryCours> CategoriesCours { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<CommentaireVideo> CommentairesVideo { get; set; }
        public DbSet<ReponseCommentaire> ReponsesCommentaire { get; set; }
        public DbSet<Examen> Examens { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choix> Choix { get; set; }
        public DbSet<Certificat> Certificats { get; set; }
        public DbSet<Soumission> Soumissions { get; set; }
        public DbSet<CoursCommence> CoursCommences { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Map Etudiant to its table
            builder.Entity<Etudiant>().ToTable("Etudiants");

            // Map Enseignant to its table
            builder.Entity<Enseignant>().ToTable("Enseignants");
            builder.Entity<CategoryCours>().HasData(
               new CategoryCours
               {
                   Id = 1,
                   Name = "Programming",
                   Description = "Courses related to programming languages and software development."
               },
               new CategoryCours
               {
                   Id = 2,
                   Name = "Data Science",
                   Description = "Courses related to data analysis, machine learning, and statistics."
               },
               new CategoryCours
               {
                   Id = 3,
                   Name = "Web Development",
                   Description = "Courses related to building websites and web applications."
               }
           );
        }
    }
}
