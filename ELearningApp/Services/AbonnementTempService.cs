using ELearningApp.IServices;
using ELearningApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class AbonnementTempService : IAbonnementTempService
    {
        private readonly List<AbonnementTemp> _abonnements = new();

        public Task<List<AbonnementTemp>> GetAllAsync()
        {
            return Task.FromResult(_abonnements);
        }

        public Task<AbonnementTemp> GetByIdAsync(int id)
        {
            return Task.FromResult(_abonnements.Find(a => a.Id == id));
        }

        public Task AddAsync(AbonnementTemp abonnementTemp)
        {
            _abonnements.Add(abonnementTemp);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(AbonnementTemp abonnementTemp)
        {
            var existing = _abonnements.Find(a => a.Id == abonnementTemp.Id);
            if (existing != null)
            {
                existing.IdAbonnement = abonnementTemp.IdAbonnement;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _abonnements.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }
    }
}
