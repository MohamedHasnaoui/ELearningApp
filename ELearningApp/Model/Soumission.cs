using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Soumission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'assignement est obligatoire.")]
        [ForeignKey("Examen")]
        [Display(Name = "Examen")]
        public int ExamenId { get; set; }

        public Examen Examen { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est obligatoire.")]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Etudiant")]
        public string EtudiantId { get; set; }

        public ApplicationUser Etudiant { get; set; }

        [Required(ErrorMessage = "La date de soumission est obligatoire.")]
        [Display(Name = "Date de soumission")]
        public DateTime DateSoumission { get; set; }

        [Display(Name = "Lien de la soumission")]
        public string LienSoumission { get; set; } // Lien vers un fichier ou une réponse sous forme de texte.

        [Display(Name = "Note obtenue")]
        [Range(0, 100, ErrorMessage = "La note doit être entre 0 et 100.")]
        public float? Note { get; set; } // Note donnée par le correcteur, facultative à la soumission.

        [Display(Name = "Commentaire du correcteur")]
        [StringLength(500, ErrorMessage = "Le commentaire ne peut pas dépasser 500 caractères.")]
        public string CommentaireCorrecteur { get; set; }
    }
}
