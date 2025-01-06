using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AjouterRatingAsync(int courseId, string etudiantId, int valeur)
        {
            // Vérifiez si une évaluation existe déjà
            var existant = await _context.Ratings
                .FirstOrDefaultAsync(r => r.CoursId == courseId && r.EtudiantId == etudiantId);

            if (existant != null)
            {
                throw new InvalidOperationException("L'étudiant a déjà évalué ce cours.");
            }

            // Ajoutez la nouvelle évaluation
            var rating = new Rating
            {
                CoursId = courseId,
                EtudiantId = etudiantId,
                Valeur = valeur
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<Rating?> ObtenirRatingAsync(int courseId, string etudiantId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.CoursId == courseId && r.EtudiantId == etudiantId);
        }

        public async Task<double> ObtenirMoyennePourCoursAsync(int courseId)
        {
            return await _context.Ratings
                .Where(r => r.CoursId == courseId)
                .AverageAsync(r => r.Valeur);
        }
        public async Task SupprimerRatingAsync(int courseId, string etudiantId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.CoursId == courseId && r.EtudiantId == etudiantId);

            if (rating == null)
            {
                throw new KeyNotFoundException("Évaluation introuvable.");
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }
        public async Task MettreAJourRatingAsync(int courseId, string etudiantId, int valeur)
        {
            var rating = await ObtenirRatingAsync(courseId, etudiantId);
            if (rating == null)
            {
                throw new KeyNotFoundException("Évaluation introuvable.");
            }

            rating.Valeur = valeur;
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RatingExisteAsync(int courseId, string etudiantId)
        {
            return await _context.Ratings
                .AnyAsync(r => r.CoursId == courseId && r.EtudiantId == etudiantId);
        }

    }
}
