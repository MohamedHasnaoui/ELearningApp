using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Assignement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre de l'assignement est obligatoire.")]
        [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères.")]
        [Display(Name = "Titre")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "La date de création est obligatoire.")]
        [Display(Name = "Date de création")]
        public DateTime DateCreation { get; set; }

        [Required(ErrorMessage = "Le cours associé est obligatoire.")]
        [ForeignKey("Cours")]
        [Display(Name = "Cours")]
        public int CoursId { get; set; }

        public Cours Cours { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
