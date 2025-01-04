using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Model
{
    public class AbonnementTemp
    {
       public int Id { get;set; }
        [Required]
        public int IdAbonnement {  get;set; }  
    }
}
