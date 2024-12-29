using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IQuestionService
    {
        Task<Question> GetByIdAsync(int id); 
        Task<IEnumerable<Question>> GetAllAsync(); 
        Task<IEnumerable<Question>> GetByExamentIdAsync(int ExamentId); 
        Task<Question> CreateAsync(Question question); 
        Task<Question> UpdateAsync(Question question);
        Task<bool> DeleteAsync(int id);
    }
}
