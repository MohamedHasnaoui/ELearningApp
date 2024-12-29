using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IChoixService
    {
        Task<Choix> GetByIdAsync(int id);
        Task<IEnumerable<Choix>> GetAllAsync();
        Task<IEnumerable<Choix>> GetByQuestionIdAsync(int questionId); 
        Task<Choix> CreateAsync(Choix choix); 
        Task<Choix> UpdateAsync(Choix choix); 
        Task<bool> DeleteAsync(int id); 
    }
}
