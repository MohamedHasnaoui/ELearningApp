using ELearningApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public interface IMentorRatingService
    {
        Task<IEnumerable<MentorRating>> GetAllRatingsAsync();
        Task<MentorRating> GetRatingByIdAsync(string id);
        Task<IEnumerable<MentorRating>> GetRatingsByMentorIdAsync(string mentorId);
        Task<IEnumerable<MentorRating>> GetRatingsByEtudiantIdAsync(string etudiantId);
        Task<double> GetAverageRatingForMentorAsync(string mentorId);
        Task AddRatingAsync(MentorRating rating);
        Task UpdateRatingAsync(MentorRating rating);
        Task DeleteRatingAsync(string id);
    }
}
