using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IRatingService
    {
        Task AjouterRatingAsync(int courseId, string etudiantId, int valeur);
        Task<Rating?> ObtenirRatingAsync(int courseId, string etudiantId);
        Task<double> ObtenirMoyennePourCoursAsync(int courseId);
        Task MettreAJourRatingAsync(int courseId, string etudiantId, int valeur);
        Task<bool> RatingExisteAsync(int courseId, string etudiantId);
    }

}
