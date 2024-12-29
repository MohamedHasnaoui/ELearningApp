using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICoursService
    {
        Task<Cours> GetByIdAsync(int id); 
        Task<IEnumerable<Cours>> GetAllAsync();
        Task<IEnumerable<Cours>> GetCoursByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Cours>> GetCoursByEnseignantIdAsync(string enseignantId);
        Task<Cours> CreateAsync(Cours cours);
        Task<Cours> UpdateAsync(Cours cours);
        Task<bool> DeleteAsync(int id);
    }
}
