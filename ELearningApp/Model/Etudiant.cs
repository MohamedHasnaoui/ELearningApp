using ELearningApp.Data;

namespace ELearningApp.Model
{
    public class Etudiant : ApplicationUser
    {
        public ICollection<Certificat> Certificats { get; set; }
        public ICollection<CoursCommence> CoursCommences { get; set; }
        public ICollection<Soumission> Soumissions { get; set; }
        public ICollection<AbonnementAchete> AbonnementsAchetes { get; set; }

        public ICollection<Enseignant> LikedEnseignants { get; set; }
        public ICollection<Enseignant> FollowedEnseignants { get; set; }

        public Etudiant()
        {
            Certificats = new HashSet<Certificat>();
            CoursCommences = new HashSet<CoursCommence>();
            Soumissions = new HashSet<Soumission>();
            AbonnementsAchetes = new HashSet<AbonnementAchete>();
            LikedEnseignants = new HashSet<Enseignant>();
            FollowedEnseignants = new HashSet<Enseignant>();
        }
    }
}