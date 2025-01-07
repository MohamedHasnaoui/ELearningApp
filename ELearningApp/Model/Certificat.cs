using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Certificat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "La date d'obtention est obligatoire.")]
        [Display(Name = "Date d'obtention")]
        public DateTime DateObtention { get; set; }

        [Required(ErrorMessage = "Le cours associé est obligatoire.")]
        [ForeignKey("Cours")]
        [Display(Name = "Cours")]
        public int CoursId { get; set; }

        public Cours Cours { get; set; }

        [Required(ErrorMessage = "L'utilisateur associé est obligatoire.")]
        [ForeignKey("Etudiant")]
        [Display(Name = "Etudiant")]
        public string EtudiantId { get; set; }

        public Etudiant Etudiant { get; set; }
    }
}
