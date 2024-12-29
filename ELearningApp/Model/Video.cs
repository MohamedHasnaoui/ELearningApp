using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Le titre de la vidéo est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre de la vidéo ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Titre de la vidéo")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "La description de la section ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Description de la section")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'URL de la vidéo est obligatoire.")]
        [Url(ErrorMessage = "L'URL de la vidéo n'est pas valide.")]
        [Display(Name = "URL de la vidéo")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = "La section associée à cette vidéo est obligatoire.")]
        [ForeignKey("Section")]
        [Display(Name = "Section")]
        public int SectionId { get; set; }

        public Section Section { get; set; }

        public ICollection<CommentaireVideo> Commentaires { get; set; }

    }
}
