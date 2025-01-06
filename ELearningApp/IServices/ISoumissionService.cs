using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ISoumissionService
    {
        Task<IEnumerable<Soumission>> GetAllAsync();
        Task<Soumission> GetByIdAsync(int id);
        Task<IEnumerable<Soumission>> GetByEtudiantIdAsync(string etudiantId);
        Task<IEnumerable<Soumission>> GetByExamenIdAsync(int assignementId);
        public Task<List<Soumission>> GetByEtudiantAndExamenIdAsync(string etudiantId, int examId);
        Task<Soumission> AddAsync(Soumission soumission);
        Task<Soumission> UpdateAsync(Soumission soumission);
        Task<bool> DeleteAsync(int id);
    }
}
