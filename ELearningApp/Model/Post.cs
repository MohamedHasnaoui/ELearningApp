using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Model
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string? titre { get; set; }

        [Required]
        public string? contenu { get; set; }

        public string? image { get; set; }

        public string? videoUrl { get; set; }

        [Required]
        public DateTime datePub { get; set; } = DateTime.Now;

        [Required]
        public string? EnseignantId { get; set; }

        public Enseignant? Enseignant { get; set; }
    }
}
