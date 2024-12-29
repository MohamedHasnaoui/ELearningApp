using ELearningApp.Model;

namespace ELearningApp.IServices
{
    public interface ICommentaireVideoService
    {
        Task<CommentaireVideo> GetByIdAsync(int id); 
        Task<IEnumerable<CommentaireVideo>> GetByVideoIdAsync(int videoId); 
        Task<CommentaireVideo> CreateAsync(CommentaireVideo commentaire); 
        Task<CommentaireVideo> UpdateAsync(CommentaireVideo commentaire);
        Task<bool> DeleteAsync(int id); // Delete a comment
    }
}
