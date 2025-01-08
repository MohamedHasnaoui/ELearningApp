using ELearningApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public interface IMentorFollowService
    {
        Task<IEnumerable<MentorFollow>> GetAllFollowsAsync();
        Task<IEnumerable<MentorFollow>> GetFollowersByMentorIdAsync(string mentorId);
        Task<IEnumerable<MentorFollow>> GetMentorsFollowedByEtudiantAsync(string etudiantId);
        Task<bool> IsFollowingAsync(string etudiantId, string mentorId);
        Task ToggleFollowMentorAsync(string etudiantId, string mentorId);
    }
}
