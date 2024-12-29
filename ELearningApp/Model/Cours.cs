using ELearningApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Cours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Le nom du cours est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom du cours ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Nom du cours")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "La description du cours est obligatoire.")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 5, ErrorMessage = "L'évaluation doit être comprise entre 0 et 5.")]
        [Display(Name = "Évaluation")]
        public float Evaluation { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire.")]
        [ForeignKey("Category")]
        [Display(Name = "Identifiant de la catégorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Catégorie")]
        public CategoryCours Category { get; set; }

        [Required(ErrorMessage = "Le créateur du cours est obligatoire.")]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Identifiant de la catégorie")]
        public string CreateurId { get; set; }

        [Display(Name = "Enseignant")]
        public ApplicationUser Enseignant { get; set; }

        public Examen Examen { get; set; }
    }
}
