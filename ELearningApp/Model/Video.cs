using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; } 

        [Required(ErrorMessage = "Le titre de la vidéo est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre de la vidéo ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Titre de la vidéo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "L'URL de la vidéo est obligatoire.")]
        [Url(ErrorMessage = "L'URL de la vidéo n'est pas valide.")]
        [Display(Name = "URL de la vidéo")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = "Public Id de la vidéo est obligatoire.")]
        [Display(Name = "VidPublicId")]
        public string VidPublicId { get; set; }

        [Required(ErrorMessage = "Duree de la vidéo est obligatoire.")]
        [Display(Name = "Duree")]
        public double Duree { get; set; }

        [Required(ErrorMessage = "La section associée à cette vidéo est obligatoire.")]
        [ForeignKey("Section")]
        [Display(Name = "Section")]
        public int SectionId { get; set; }

        [Required(ErrorMessage = "L'image' associée à cette vidéo est obligatoire.")]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }
        public Section Section { get; set; }

        public ICollection<CommentaireVideo> Commentaires { get; set; }

        public string FormatDuration()
        {
            int minutes = (int)(this.Duree / 60);
            int remainingSeconds = (int)(Duree % 60);
            return $"{minutes:D2} min {remainingSeconds:D2} sec"; // Format as mm:ss
        }
    }
}
