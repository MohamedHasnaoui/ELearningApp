using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrément
        public int Id { get; set; }

        [Required(ErrorMessage = "L'énoncé de la question est obligatoire.")]
        [StringLength(500, ErrorMessage = "La question ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Énoncé")]
        public string Enonce { get; set; }

        [Required(ErrorMessage = "L'assignement associé est obligatoire.")]
        [ForeignKey("Assignement")]
        [Display(Name = "Assignement")]
        public int AssignementId { get; set; }

        public Assignement Assignement { get; set; }

        public ICollection<Choix> Choix { get; set; }
    }
}
