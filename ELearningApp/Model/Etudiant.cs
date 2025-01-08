using Blazorise;
using ELearningApp.Data;

namespace ELearningApp.Model
{
    public class Etudiant: ApplicationUser
    {
        public ICollection<Certificat> Certificats { get; set; }
        public ICollection<CoursCommence> CoursCommences { get; set; }
        public ICollection<Soumission> Soumissions { get; set; }
        public ICollection<AbonnementAchete> AbonnementsAchetes { get; set; }
        public ICollection<MentorRating> Ratings { get; set; }
        public ICollection<MentorFollow> Following { get; set; }

        public Etudiant()
        {
            Certificats = new HashSet<Certificat>();
            CoursCommences = new HashSet<CoursCommence>();
            Soumissions = new HashSet<Soumission>();
        }
    }
}
