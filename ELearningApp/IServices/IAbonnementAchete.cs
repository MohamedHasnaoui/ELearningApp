using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IAbonnementAchete
    {
         Task<AbonnementAchete> GetAbonnementAcheteById(string idEtudiant, int idAbonnement);
        Task<List<AbonnementAchete>> GetAllAbonnementsAchetes();
        Task AddAbonnementAchete(AbonnementAchete abonnementAchete);
        Task UpdateAbonnementAchete(AbonnementAchete abonnementAchete);
        Task DeleteAbonnementAchete(string idEtudiant, int idAbonnement);
        Task<bool> IsAbonnementAchete(int idAbonnement, string etudiantId);
         }

}
