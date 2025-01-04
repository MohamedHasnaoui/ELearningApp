
using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IAbonnementTempService
    {
        Task<List<AbonnementTemp>> GetAllAsync();
        Task<AbonnementTemp> GetByIdAsync(int id);
        Task AddAsync(AbonnementTemp abonnementTemp);
        Task UpdateAsync(AbonnementTemp abonnementTemp);
        Task DeleteAsync(int id);
    }

}
