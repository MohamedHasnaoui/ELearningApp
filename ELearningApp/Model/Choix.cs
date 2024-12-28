using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Choix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "Le texte du choix est obligatoire.")]
        [StringLength(200, ErrorMessage = "Le choix ne peut pas dépasser 200 caractères.")]
        [Display(Name = "Texte du choix")]
        public string Texte { get; set; }

        [Required(ErrorMessage = "La question associée est obligatoire.")]
        [ForeignKey("Question")]
        [Display(Name = "Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required(ErrorMessage = "Indiquez si le choix est correct ou non.")]
        [Display(Name = "Est Correct ?")]
        public bool EstCorrect { get; set; }
    }
}
