using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICategoryCoursService
    {
        Task<IEnumerable<CategoryCours>> GetAllCategoriesAsync();
        Task<CategoryCours> GetCategoryByIdAsync(int id);
        Task<CategoryCours> AddCategoryAsync(CategoryCours category);
        Task<CategoryCours> UpdateCategoryAsync(CategoryCours category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
