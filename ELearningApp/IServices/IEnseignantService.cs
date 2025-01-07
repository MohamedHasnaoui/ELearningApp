namespace ELearningApp.IServices
{
    public interface IEnseignantService
    {
        Task<(int totalCours, int totalEtudiants, double totalEvaluations)> GetEnseignantStatsAsync(string enseignantId);

    }
}
