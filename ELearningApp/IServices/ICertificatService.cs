using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICertificatService
    {
        Task<IEnumerable<Certificat>> GetAllAsync();
        Task<Certificat?> GetByIdAsync(int id);
        Task<IEnumerable<Certificat>> GetByEtudiantIdAsync(string etudiantId);
        Task<Certificat?> GetByEtudiantIdCoursIdAsync(string etudiantId, int coursId);
        Task<Certificat> AddAsync(Certificat certificat);
        Task<Certificat> UpdateAsync(Certificat certificat);
        Task<bool> DeleteAsync(int id);
    }
}
