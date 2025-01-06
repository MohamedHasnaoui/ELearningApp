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
        [ForeignKey("Etudiant")]
        [Display(Name = "Etudiant")]
        public string EtudiantId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required(ErrorMessage = "La date de soumission est obligatoire.")]
        [Display(Name = "Date de soumission")]
        public DateTime DateSoumission { get; set; }

        [Display(Name = "Note obtenue")]
        [Range(0, 100, ErrorMessage = "La note doit être entre 0 et 100.")]
        public int Note { get; set; } // Note donnée par le correcteur, facultative à la soumission.

    }
}
