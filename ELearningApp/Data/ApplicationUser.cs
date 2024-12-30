using Microsoft.AspNetCore.Identity;

namespace ELearningApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? imgProfile { get; set; }
        public string? imgCover { get; set; }
        public string? FormalUserName { get; set; }
        public DateTime joinDate { get; set; } = DateTime.UtcNow;

    }

}
