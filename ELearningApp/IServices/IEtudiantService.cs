using ELearningApp.Model;
using static ELearningApp.Services.EtudiantService;

namespace ELearningApp.IServices
{
    public interface IEtudiantService
    {
        Task<int[]> GetDailyRegistrationsAsync();
        Task<int[]> GetRegistrationsBy2HourIntervalsAsync();
        Task<int[]> GetMonthlyStudentRegistrationsAsync();
        Task<List<Etudiant>> GetAllEtudiantsAsync();
        Task<PaginatedResult<Etudiant>> GetPaginatedEtudiantsAsync(int pageNumber, int pageSize);
        Task<Etudiant?> GetEtudiantByIdAsync(string etudiantId);
        Task<Etudiant?> CreateEtudiantAsync(Etudiant etudiant, string password);
        Task<bool> UpdateEtudiantAsync(Etudiant etudiant);
        Task<bool> DeleteEtudiantAsync(string etudiantId);
        Task<bool> BlockEtudiantAsync(string etudiantId);
        Task<List<Etudiant>> GetBlockedEtudiantsAsync();
        Task<List<Etudiant>> GetActiveEtudiantsAsync();
    }
}
