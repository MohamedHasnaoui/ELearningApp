using ELearningApp.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ELearningApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? imgProfile { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumberCode { get; set; }
        public string? Bio { get; set; }
        public string? imgCover { get; set; }
        public string? FormalUserName { get; set; }
        public DateTime joinDate { get; set; } = DateTime.UtcNow;
        public ICollection<CommentaireVideo> CommentairesVideos { get; set; }
        public ICollection<ReponseCommentaire> ReponsesCommentaires { get; set; }
        public ApplicationUser()
        {
            CommentairesVideos = new HashSet<CommentaireVideo>();
            ReponsesCommentaires = new HashSet<ReponseCommentaire>();
        }
    }

}
