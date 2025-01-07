using ELearningApp.Model;
using ELearningApp.Services;

namespace ELearningApp.IServices
{
    public interface ICoursService
    {
        Task<Cours> GetByIdAsync(int id);
        Task<IEnumerable<Cours>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Cours>> GetCoursByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
        Task<IEnumerable<Cours>> GetCoursByEnseignantIdAsync(string enseignantId, int pageNumber, int pageSize);
        Task<List<Cours>> GetTop3RatedCoursByEnseignantIdAsync(string enseignantId);
        Task<Cours> CreateAsync(Cours cours);
        Task<Cours> UpdateAsync(Cours cours);
        Task<bool> DeleteAsync(int id);
        Task<int> CountCourses();
        Task<int> CountByEnseignantId(string enseignantId);
        Task<int> CountByCategoryIdAsync(int categoryId);
        Task<List<EtudiantCoursInfo>> GetEtudiantsInscritsAsync(string enseignantId);
        // Task<List<EtudiantCoursInfo>> GetEtudiantsInscritsFiltrésParStatutAsync(string enseignantId, string statut);
        Task<List<TopCoursDto>> GetTop5NbEtudinatCoursByEnseignantAsync(string enseignantId);
        Task<List<TopCoursDto>> GetTop5EvaluationCoursByEnseignantAsync(string enseignantId);
        Task<List<Cours>> SearchCoursesByTitleAsync(string partialTitle, int pageNumber, int pageSize);
        Task<List<Cours>> SearchCoursesByTitleAndCategoryIdAsync(string partialTitle, int categoryId, int pageNumber, int pageSize);
        Task<int> CountCoursesByTitleAsync(string partialTitle);
        Task<int> CountCoursesByTitleAndCategoryIdAsync(string partialTitle, int categoryId);
    }
}
