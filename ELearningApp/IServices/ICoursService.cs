﻿using ELearningApp.Model;
using ELearningApp.Services;

namespace ELearningApp.IServices
{
    public interface ICoursService
    {
        Task<Cours> GetByIdAsync(int id); 
        Task<IEnumerable<Cours>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Cours>> GetCoursByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
        Task<IEnumerable<Cours>> GetCoursByEnseignantIdAsync(string enseignantId, int pageNumber, int pageSize);
        Task<Cours> CreateAsync(Cours cours);
        Task<Cours> UpdateAsync(Cours cours);
        Task<bool> DeleteAsync(int id);
        Task<int> CountCourses();
        Task<int> CountByEnseignantId(string enseignantId);
        Task<int> CountByCategoryIdAsync(int categoryId);
        Task<List<EtudiantCoursInfo>> GetEtudiantsInscritsAsync(string enseignantId);
    }
}
