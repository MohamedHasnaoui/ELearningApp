using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class EnseignantService : IEnseignantService
    {
        private readonly ApplicationDbContext _context;

        public EnseignantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(int totalCours, int totalEtudiants, double totalEvaluations)> GetEnseignantStatsAsync(string enseignantId)
        {
            var cours = await _context.Cours
                .Include(c => c.sections)
                .Where(c => c.EnseignantId == enseignantId)
                .ToListAsync();

            int totalCours = cours.Count;

            int totalEtudiants = await _context.CoursCommences
                .Where(cc => cours.Select(c => c.Id).Contains(cc.CoursId))
                .Select(cc => cc.EtudiantId)
                .Distinct()
                .CountAsync();

            double totalEvaluations = cours.Sum(c => c.Evaluation);

            return (totalCours, totalEtudiants, totalEvaluations);
        }
    }
}
