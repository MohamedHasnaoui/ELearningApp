using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class MentorFollow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Etudiant")]
        public string EtudiantId { get; set; } // Student who is following

        public Etudiant Etudiant { get; set; }

        [Required]
        [ForeignKey("Mentor")]
        public string EnseignantId { get; set; } // Mentor being followed

        public Enseignant Enseignant { get; set; }
    }
}
