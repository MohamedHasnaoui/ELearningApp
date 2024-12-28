using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;

namespace ELearningApp.Model
{
    public class ReponseCommentaire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Le contenu de la réponse est obligatoire.")]
        [StringLength(1000, ErrorMessage = "La réponse ne peut pas dépasser 1000 caractères.")]
        [Display(Name = "Contenu de la réponse")]
        public string Contenu { get; set; }

        // Date de création de la réponse
        [Required(ErrorMessage = "La date de la réponse est obligatoire.")]
        [Display(Name = "Date de la réponse")]
        public DateTime DateCreation { get; set; }

        [Required(ErrorMessage = "Le commentaire auquel cette réponse appartient est obligatoire.")]
        [ForeignKey("Commentaire")]
        [Display(Name = "Commentaire")]
        public int CommentaireId { get; set; }

        public CommentaireVideo Commentaire { get; set; }

        [Required(ErrorMessage = "L'utilisateur qui a fait la réponse est obligatoire.")]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Utilisateur")]
        public string UtilisateurId { get; set; } 

        public ApplicationUser Utilisateur { get; set; }
    }
}
