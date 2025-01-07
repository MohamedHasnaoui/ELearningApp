using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IExamenService
    {
        Task<Examen> GetByIdAsync(int id);
        Task<IEnumerable<Examen>> GetAllAsync(); 
        Task<Examen> GetByCoursIdAsync(int coursId); 
        Task<Examen> CreateAsync(Examen examen); 
        Task<Examen> UpdateAsync(Examen examen); 
        Task<bool> DeleteAsync(int id);
    }
}
