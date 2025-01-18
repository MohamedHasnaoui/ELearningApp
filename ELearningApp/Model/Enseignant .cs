using ELearningApp.Data;


namespace ELearningApp.Model
{

    
    public class Enseignant : ApplicationUser
    {
        public string? speciality { get; set; }
        public ICollection<Cours> CoursCrees { get; set; }
        public ICollection<MentorFollow> Followers { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Enseignant()
        {
            CoursCrees = new HashSet<Cours>();
            Posts = new HashSet<Post>();
        }
        public ICollection<MentorRating> Ratings { get; set; }
    }
}
