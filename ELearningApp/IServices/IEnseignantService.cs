using ELearningApp.Model;
using static ELearningApp.Services.EnseignantService;

namespace ELearningApp.IServices
{
    public interface IEnseignantService
    {
        Task<int[]> GetDailyRegistrationsAsync();
        Task<int[]> GetRegistrationsBy2HourIntervalsAsync();
        Task<int[]> GetMonthlyMentorsRegistrationsAsync();
        Task<List<Enseignant>> GetAllEnseignantsAsync();
        Task<PaginatedResult2<Enseignant>> GetPaginatedEnseignantsAsync(int pageNumber, int pageSize);
        Task<(int totalCours, int totalEtudiants, double totalEvaluations, int ratedCourses, int totaleRating)> GetEnseignantStatsAsync(string enseignantId);

        Task<Enseignant?> GetEnseignantByIdAsync(string enseignantId);
        Task<Enseignant?> CreateEnseignantAsync(Enseignant enseignant, string password);
        Task<bool> UpdateEnseignantAsync(Enseignant enseignant);
        Task<bool> DeleteEnseignantAsync(string enseignantId);
        Task<bool> BlockEnseignantAsync(string enseignantId);
        Task<List<Enseignant>> GetBlockedEnseignantsAsync();
        Task<List<Enseignant>> GetActiveEnseignantsAsync();
    }
}
