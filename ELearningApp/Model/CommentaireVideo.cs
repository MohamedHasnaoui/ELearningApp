using Azure;
using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class CommentaireVideo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clé primaire

        [Required(ErrorMessage = "Le contenu du commentaire est obligatoire.")]
        [StringLength(1000, ErrorMessage = "Le commentaire ne peut pas dépasser 1000 caractères.")]
        [Display(Name = "Contenu du commentaire")]
        public string Contenu { get; set; }

        // Date de création du commentaire
        [Required(ErrorMessage = "La date du commentaire est obligatoire.")]
        [Display(Name = "Date du commentaire")]
        public DateTime DateCreation { get; set; }

        // Relation avec la vidéo
        [Required(ErrorMessage = "La vidéo associée à ce commentaire est obligatoire.")]
        [ForeignKey("Video")]
        [Display(Name = "Vidéo")]
        public int VideoId { get; set; }

        // Navigation vers la vidéo
        public Video Video { get; set; }

        // Relation avec l'utilisateur (si vous avez une classe Utilisateur)
        [Required(ErrorMessage = "L'utilisateur qui a fait le commentaire est obligatoire.")]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Utilisateur")]
        public string UtilisateurId { get; set; }

        // Navigation vers l'utilisateur
        public ApplicationUser Utilisateur { get; set; }

        // Relation avec les réponses
        public ICollection<ReponseCommentaire> Reponses { get; set; }
    }
}
