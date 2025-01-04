using ELearningApp.Model;
namespace ELearningApp.IServices
{
    public interface IAbonnementService
    {
        Task<List<Abonnement>> GetAllAbonnementsAsync();
        Task<Abonnement> GetAbonnementByIdAsync(int id);
        Task CreateAbonnementAsync(Abonnement abonnement);
        Task UpdateAbonnementAsync(Abonnement abonnement);
        Task DeleteAbonnementAsync(int id);
        Task<string> CreateCheckoutSession(int id,string Plan, int prix);
    }
}
