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
        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<AbonnementTemp> AbonnementTemps { get; set; }
        public DbSet<AbonnementAchete> AbonnementsAchetes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MentorRating> MentorRatings { get; set; }
        public DbSet<MentorFollow> MentorFollows { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Map Etudiant to its table
            builder.Entity<Etudiant>().ToTable("Etudiants");
            /* builder.Entity<AbonnementAchete>()
             .HasKey(a => new { a.IdEtudiant, a.IdAbonnement });*/

            
            // Map Enseignant to its table
            builder.Entity<Enseignant>().ToTable("Enseignants");
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
            builder.Entity<Rating>()
            .HasIndex(r => new { r.CoursId, r.EtudiantId })
            .IsUnique();

            builder.Entity<Section>()
            .HasOne(s => s.Cours)
            .WithMany(c => c.sections)
            .HasForeignKey(s => s.CoursId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for sections

            // Configure cascade delete for Videos when Section is deleted
            builder.Entity<Video>()
                .HasOne(v => v.Section)
                .WithMany(s => s.Videos)
                .HasForeignKey(v => v.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
