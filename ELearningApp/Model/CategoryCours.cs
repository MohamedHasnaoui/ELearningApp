using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class CategoryCours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la catégorie est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom de la catégorie ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Nom de la catégorie")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "La description ne peut pas dépasser 300 caractères.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Cours associés")]
        public ICollection<Cours> Courses { get; set; }
    }
}
