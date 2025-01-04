using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clé primaire

        [Required(ErrorMessage = "Le titre de la section est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre de la section ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Titre de la section")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "La description de la section ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Description de la section")]
        public string Description { get; set; }

        // Relation avec le cours
        [Required(ErrorMessage = "Le cours associé à cette section est obligatoire.")]
        [ForeignKey("Cours")]
        [Display(Name = "Cours")]
        public int CoursId { get; set; }

        [Display(Name = "Duree")]
        public double? Duree { get; set; }

        // Navigation vers le cours
        public Cours Cours { get; set; }

        // Relation avec les vidéos
        public ICollection<Video> Videos { get; set; }
    }
}
