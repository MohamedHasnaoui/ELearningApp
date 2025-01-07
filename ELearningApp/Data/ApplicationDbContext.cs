using ELearningApp.Model;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<AbonnementTemp> AbonnementTemps { get; set; }
        public DbSet<AbonnementAchete> AbonnementsAchetes { get; set; }

        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Map Etudiant to its table
            builder.Entity<Etudiant>().ToTable("Etudiants");        

            // Map Enseignant to its table
            builder.Entity<Enseignant>().ToTable("Enseignants");
            // Many-to-Many: Etudiant follows Enseignant
            builder.Entity<Etudiant>()
                .HasMany(e => e.FollowedEnseignants)
                .WithMany(e => e.FollowedByEtudiants)
                .UsingEntity(j => j.ToTable("EnseignantFollowers"));

            // Many-to-Many: Etudiant likes Enseignant
            builder.Entity<Etudiant>()
                .HasMany(e => e.LikedEnseignants)
                .WithMany(e => e.LikedByEtudiants)
                .UsingEntity(j => j.ToTable("EnseignantLikes"));

            builder.Entity<Abonnement>().ToTable("Abonnements");

            builder.Entity<AbonnementTemp>().ToTable("AbonnementTemps");
            builder.Entity<AbonnementAchete>().ToTable("AbonnementAchetes")
            .HasOne(a => a.Etudiant)
            .WithMany(e => e.AbonnementsAchetes)
            .HasForeignKey(a => a.IdEtudiant)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AbonnementAchete>()
           .HasOne(a => a.Abonnement)
           .WithMany(ab => ab.AbonnementsAchetes)
           .HasForeignKey(a => a.IdAbonnement)
           .OnDelete(DeleteBehavior.Restrict);
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
            builder.Entity<Abonnement>().HasData(
        new Abonnement
        {
            Id = 1,
            Type = TypeAbonnement.Basique,
            Duree = DureeAbonnement.an,
            Prix = 199,
            IsRecommanded = false,
            Description = "Perfect plan for students",
            Caracteristiques = "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing"
        },
        new Abonnement
        {
            Id = 2,
            Type = TypeAbonnement.Standard,
            Duree = DureeAbonnement.an,
            Prix = 299,
            IsRecommanded = true,
            Description = "For users who want to do more",
            Caracteristiques = "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing, Access to course community"
        },
        new Abonnement
        {
            Id = 3,
            Type = TypeAbonnement.Premium,
            Duree = DureeAbonnement.an,
            Prix = 499,
            IsRecommanded = false,
            Description = "Your entire friends in one place",
            Caracteristiques = "Intro video the course, Interactive quizzes, Course curriculum, Community supports, Certificate of completion, Sample lesson showcasing, Access to course community"
        }
    );
        }
    }
}
