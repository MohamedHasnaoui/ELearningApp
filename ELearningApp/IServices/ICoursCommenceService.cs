using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICoursCommenceService
    {
        Task<IEnumerable<CoursCommence>> GetAllAsync();
        Task<CoursCommence> GetByIdAsync(int id);
        Task<IEnumerable<CoursCommence>> GetByCoursIdAsync(int coursId);
        Task<IEnumerable<CoursCommence>> GetByEtudiantIdAsync(string etudiantId);
        Task<CoursCommence> GetByEtudiantAndCoursAsync(string etudiantId, int coursId);
        Task<CoursCommence> AddAsync(CoursCommence coursCommence);
        Task<CoursCommence> UpdateAsync(CoursCommence coursCommence);
        Task<bool> DeleteAsync(int id);
    }
}
