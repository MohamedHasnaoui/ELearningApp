using ELearningApp.Data;


namespace ELearningApp.Model
{

    
    public class Enseignant : ApplicationUser
    {
        public string? speciality { get; set; }
        public ICollection<Cours> CoursCrees { get; set; }
        public Enseignant()
        {
            CoursCrees = new HashSet<Cours>();
        }
    }
}
