using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class CoursCommence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "L'identifiant du cours est obligatoire.")]
        [ForeignKey("Cours")]
        [Display(Name = "Cours")]
        public int CoursId { get; set; }

        public Cours Cours { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est obligatoire.")]
        [ForeignKey("Etudiant")]
        [Display(Name = "Etudiant")]
        public string EtudiantId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire.")]
        [Display(Name = "Date de début")]
        public DateTime DateDebut { get; set; }

        [Display(Name = "Progression (%)")]
        [Range(0, 100, ErrorMessage = "La progression doit être comprise entre 0 et 100.")]
        public int Progres { get; set; }

        [Display(Name = "nombreVidRegarde")]
        public int nbWatchedVid { get; set; }

        [Display(Name = "Date de fin")]
        public DateTime? DateFin { get; set; } 
    }
}
