using ELearningApp.Model;
namespace ELearningApp.IServices
{
    public interface ISectionService
    {
        Task<Section> GetByIdAsync(int id); 
        Task<IEnumerable<Section>> GetSectionsByCoursIdAsync(int coursId); 
        Task<bool> CreateAsync(Section section); 
        Task<Section> UpdateAsync(Section section); 
        Task<bool> DeleteAsync(int id); 
    }
}
