using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface IReponseCommentaireService
    {
        Task<ReponseCommentaire> GetByIdAsync(int id); 
        Task<IEnumerable<ReponseCommentaire>> GetByCommentaireIdAsync(int commentaireId); 
        Task<ReponseCommentaire> CreateAsync(ReponseCommentaire reponse); 
        Task<ReponseCommentaire> UpdateAsync(ReponseCommentaire reponse);
        Task<bool> DeleteAsync(int id);
    }
}
