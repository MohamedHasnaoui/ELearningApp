using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Model
{
    public class Abonnement
    {
        public int Id { get; set; }
        [Required]
        public TypeAbonnement Type { get; set; }
        [Required]
        public DureeAbonnement Duree { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Le prix doit être supérieur ou égal à 0.")]
        public int Prix { get; set; }

        public Boolean IsRecommanded { get;set; }=false;
        [Required]
        public string? Description { get; set; }


        [Required]
        public string? Caracteristiques { get; set; }
        public ICollection<AbonnementAchete> AbonnementsAchetes { get; set; }

    }
}
