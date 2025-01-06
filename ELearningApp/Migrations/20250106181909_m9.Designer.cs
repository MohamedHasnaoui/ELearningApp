﻿// <auto-generated />
using System;
using ELearningApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ELearningApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250106181909_m9")]
    partial class m9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ELearningApp.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FormalUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumberCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("imgCover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imgProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("joinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ELearningApp.Model.Abonnement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caracteristiques")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duree")
                        .HasColumnType("int");

                    b.Property<bool>("IsRecommanded")
                        .HasColumnType("bit");

                    b.Property<int>("Prix")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Abonnements", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.AbonnementAchete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateDebutAchat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateExpiration")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAbonnement")
                        .HasColumnType("int");

                    b.Property<string>("IdEtudiant")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdAbonnement");

                    b.HasIndex("IdEtudiant");

                    b.ToTable("AbonnementAchetes", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.AbonnementTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdAbonnement")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AbonnementTemps", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.CategoryCours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CategoriesCours");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Courses related to programming languages and software development.",
                            Name = "Programming"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Courses related to data analysis, machine learning, and statistics.",
                            Name = "Data Science"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Courses related to building websites and web applications.",
                            Name = "Web Development"
                        });
                });

            modelBuilder.Entity("ELearningApp.Model.Certificat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateObtention")
                        .HasColumnType("datetime2");

                    b.Property<string>("EtudiantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CoursId");

                    b.HasIndex("EtudiantId");

                    b.ToTable("Certificats");
                });

            modelBuilder.Entity("ELearningApp.Model.Choix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EstCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Texte")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choix");
                });

            modelBuilder.Entity("ELearningApp.Model.CommentaireVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("UtilisateurId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtilisateurId");

                    b.HasIndex("VideoId");

                    b.ToTable("CommentairesVideo");
                });

            modelBuilder.Entity("ELearningApp.Model.Cours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CoursImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoursImgPublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Duree")
                        .HasColumnType("float");

                    b.Property<string>("EnseignantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Evaluation")
                        .HasColumnType("float");

                    b.Property<int>("Niveau")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("nbVids")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EnseignantId");

                    b.ToTable("Cours");
                });

            modelBuilder.Entity("ELearningApp.Model.CoursCommence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("EtudiantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Progres")
                        .HasColumnType("int");

                    b.Property<int>("nbWatchedVid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursId");

                    b.HasIndex("EtudiantId");

                    b.ToTable("CoursCommences");
                });

            modelBuilder.Entity("ELearningApp.Model.Examen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoursId")
                        .IsUnique();

                    b.ToTable("Examens");
                });

            modelBuilder.Entity("ELearningApp.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Enonce")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ExamenId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamenId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ELearningApp.Model.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<string>("EtudiantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Valeur")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EtudiantId");

                    b.HasIndex("CoursId", "EtudiantId")
                        .IsUnique();

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("ELearningApp.Model.ReponseCommentaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentaireId")
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("UtilisateurId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CommentaireId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("ReponsesCommentaire");
                });

            modelBuilder.Entity("ELearningApp.Model.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Duree")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CoursId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("ELearningApp.Model.Soumission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateSoumission")
                        .HasColumnType("datetime2");

                    b.Property<string>("EtudiantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ExamenId")
                        .HasColumnType("int");

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EtudiantId");

                    b.HasIndex("ExamenId");

                    b.ToTable("Soumissions");
                });

            modelBuilder.Entity("ELearningApp.Model.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Duree")
                        .HasColumnType("float");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VidPublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.Enseignant", b =>
                {
                    b.HasBaseType("ELearningApp.Data.ApplicationUser");

                    b.Property<string>("speciality")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Enseignants", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.Etudiant", b =>
                {
                    b.HasBaseType("ELearningApp.Data.ApplicationUser");

                    b.ToTable("Etudiants", (string)null);
                });

            modelBuilder.Entity("ELearningApp.Model.AbonnementAchete", b =>
                {
                    b.HasOne("ELearningApp.Model.Abonnement", "Abonnement")
                        .WithMany("AbonnementsAchetes")
                        .HasForeignKey("IdAbonnement")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Etudiant", "Etudiant")
                        .WithMany("AbonnementsAchetes")
                        .HasForeignKey("IdEtudiant")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Abonnement");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("ELearningApp.Model.Certificat", b =>
                {
                    b.HasOne("ELearningApp.Model.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Etudiant", "Etudiant")
                        .WithMany("Certificats")
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("ELearningApp.Model.Choix", b =>
                {
                    b.HasOne("ELearningApp.Model.Question", "Question")
                        .WithMany("Choix")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ELearningApp.Model.CommentaireVideo", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", "Utilisateur")
                        .WithMany("CommentairesVideos")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Video", "Video")
                        .WithMany("Commentaires")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("ELearningApp.Model.Cours", b =>
                {
                    b.HasOne("ELearningApp.Model.CategoryCours", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId");

                    b.HasOne("ELearningApp.Model.Enseignant", "Enseignant")
                        .WithMany("CoursCrees")
                        .HasForeignKey("EnseignantId");

                    b.Navigation("Category");

                    b.Navigation("Enseignant");
                });

            modelBuilder.Entity("ELearningApp.Model.CoursCommence", b =>
                {
                    b.HasOne("ELearningApp.Model.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Etudiant", "Etudiant")
                        .WithMany("CoursCommences")
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("ELearningApp.Model.Examen", b =>
                {
                    b.HasOne("ELearningApp.Model.Cours", "Cours")
                        .WithOne("Examen")
                        .HasForeignKey("ELearningApp.Model.Examen", "CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");
                });

            modelBuilder.Entity("ELearningApp.Model.Question", b =>
                {
                    b.HasOne("ELearningApp.Model.Examen", "Examen")
                        .WithMany("Questions")
                        .HasForeignKey("ExamenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examen");
                });

            modelBuilder.Entity("ELearningApp.Model.Rating", b =>
                {
                    b.HasOne("ELearningApp.Model.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("ELearningApp.Model.ReponseCommentaire", b =>
                {
                    b.HasOne("ELearningApp.Model.CommentaireVideo", "Commentaire")
                        .WithMany("Reponses")
                        .HasForeignKey("CommentaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Data.ApplicationUser", "Utilisateur")
                        .WithMany("ReponsesCommentaires")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commentaire");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("ELearningApp.Model.Section", b =>
                {
                    b.HasOne("ELearningApp.Model.Cours", "Cours")
                        .WithMany("sections")
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");
                });

            modelBuilder.Entity("ELearningApp.Model.Soumission", b =>
                {
                    b.HasOne("ELearningApp.Model.Etudiant", "Etudiant")
                        .WithMany("Soumissions")
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Model.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etudiant");

                    b.Navigation("Examen");
                });

            modelBuilder.Entity("ELearningApp.Model.Video", b =>
                {
                    b.HasOne("ELearningApp.Model.Section", "Section")
                        .WithMany("Videos")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ELearningApp.Model.Enseignant", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("ELearningApp.Model.Enseignant", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ELearningApp.Model.Etudiant", b =>
                {
                    b.HasOne("ELearningApp.Data.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("ELearningApp.Model.Etudiant", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ELearningApp.Data.ApplicationUser", b =>
                {
                    b.Navigation("CommentairesVideos");

                    b.Navigation("ReponsesCommentaires");
                });

            modelBuilder.Entity("ELearningApp.Model.Abonnement", b =>
                {
                    b.Navigation("AbonnementsAchetes");
                });

            modelBuilder.Entity("ELearningApp.Model.CategoryCours", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("ELearningApp.Model.CommentaireVideo", b =>
                {
                    b.Navigation("Reponses");
                });

            modelBuilder.Entity("ELearningApp.Model.Cours", b =>
                {
                    b.Navigation("Examen")
                        .IsRequired();

                    b.Navigation("sections");
                });

            modelBuilder.Entity("ELearningApp.Model.Examen", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("ELearningApp.Model.Question", b =>
                {
                    b.Navigation("Choix");
                });

            modelBuilder.Entity("ELearningApp.Model.Section", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("ELearningApp.Model.Video", b =>
                {
                    b.Navigation("Commentaires");
                });

            modelBuilder.Entity("ELearningApp.Model.Enseignant", b =>
                {
                    b.Navigation("CoursCrees");
                });

            modelBuilder.Entity("ELearningApp.Model.Etudiant", b =>
                {
                    b.Navigation("AbonnementsAchetes");

                    b.Navigation("Certificats");

                    b.Navigation("CoursCommences");

                    b.Navigation("Soumissions");
                });
#pragma warning restore 612, 618
        }
    }
}
