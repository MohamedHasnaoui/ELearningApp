using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "La note doit être comprise entre 1 et 5.")]
        public int Valeur { get; set; } // Note donnée par l'étudiant

        [Required]
        [ForeignKey("Cours")]
        [Display(Name = "CoursId")]
        public int CoursId { get; set; } // Référence au cours

        public Cours Cours { get; set; }

        [Required]
        [ForeignKey("Etudiant")]
        [Display(Name = "EtudiantId")]
        public string EtudiantId { get; set; } // Référence à l'étudiant

        public Etudiant Etudiant { get; set; }
    }
}
