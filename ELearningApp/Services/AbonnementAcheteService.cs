using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class AbonnementAcheteService : IAbonnementAchete
    {
        private readonly ApplicationDbContext _context;

        public AbonnementAcheteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour récupérer un abonnement acheté par un étudiant spécifique
        public async Task<AbonnementAchete> GetAbonnementAcheteById(string idEtudiant, int idAbonnement)
        {
            return await _context.AbonnementsAchetes
                .Include(a => a.Etudiant)
                .Include(a => a.Abonnement)
                .FirstOrDefaultAsync(a => a.IdEtudiant == idEtudiant && a.IdAbonnement == idAbonnement);
        }




        // Ajouter un abonnement acheté
        public async Task AddAbonnementAchete(AbonnementAchete abonnementAchete)
        {
            _context.AbonnementsAchetes.Add(abonnementAchete);
            await _context.SaveChangesAsync();
        }

        // Mise à jour d'un abonnement acheté
        public async Task UpdateAbonnementAchete(AbonnementAchete abonnementAchete)
        {
            _context.AbonnementsAchetes.Update(abonnementAchete);
            await _context.SaveChangesAsync();
        }

        // Supprimer un abonnement acheté
        public async Task DeleteAbonnementAchete(string idEtudiant, int idAbonnement)
        {

            var abonnementAchete = await _context.AbonnementsAchetes
                .FirstOrDefaultAsync(a => a.IdEtudiant == idEtudiant && a.IdAbonnement == idAbonnement);

            if (abonnementAchete != null)
            {
                _context.AbonnementsAchetes.Remove(abonnementAchete);
                await _context.SaveChangesAsync();
            }
        }

        // Récupérer tous les abonnements achetés
        public async Task<List<AbonnementAchete>> GetAllAbonnementsAchetes()
        {
            return await _context.AbonnementsAchetes
                .Include(a => a.Etudiant)  // Inclure l'Etudiant
                .Include(a => a.Abonnement)  // Inclure l'Abonnement
                .ToListAsync();
        }

        // Vérifier si l'abonnement a déjà été acheté par l'étudiant
public async Task<bool> IsAbonnementAchete( int idAbonnement)
{
    return await _context.AbonnementsAchetes
        .AnyAsync(a => a.IdAbonnement == idAbonnement);
}

    }

}
