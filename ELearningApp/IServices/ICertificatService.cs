using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICertificatService
    {
        Task<int[]> GetCertificatsIssuedLast10DaysAsync();
        Task<IEnumerable<Certificat>> GetAllAsync();
        Task<Certificat> GetByIdAsync(int id);
        Task<IEnumerable<Certificat>> GetByEtudiantIdAsync(string etudiantId);
        Task<Certificat> AddAsync(Certificat certificat);
        Task<Certificat> UpdateAsync(Certificat certificat);
        Task<bool> DeleteAsync(int id);
    }
}
