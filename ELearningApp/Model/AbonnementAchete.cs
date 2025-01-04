namespace ELearningApp.Model
{
    public class AbonnementAchete
    {
        public int Id { get; set; } 
        public string IdEtudiant { get; set; }
        public Etudiant Etudiant { get; set; } 

        public int IdAbonnement { get; set; }
        public Abonnement Abonnement { get; set; } 

        public DateTime DateDebutAchat { get; set; }
        public DateTime DateExpiration { get; set; }
    }

}
