using ELearningApp.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Certificat> Certificats { get; set; }
        public ICollection<CoursCommence> CoursCommences { get; set; }
        public ICollection<Cours> CoursCrees { get; set; }
        public ICollection<Soumission> Soumissions { get; set; }
        public ICollection<CommentaireVideo> CommentairesVideos { get; set; }
        public ICollection<ReponseCommentaire> ReponsesCommentaires { get; set; }
        public ApplicationUser()
        {
            Certificats = new HashSet<Certificat>();
            CoursCommences = new HashSet<CoursCommence>();
            CoursCrees = new HashSet<Cours>();
            Soumissions = new HashSet<Soumission>();
            CommentairesVideos = new HashSet<CommentaireVideo>();
            ReponsesCommentaires = new HashSet<ReponseCommentaire>();
        }
    }

}
