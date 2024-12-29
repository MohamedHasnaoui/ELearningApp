using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;
namespace ELearningApp.Services
{
    public class CommentaireVideoService : ICommentaireVideoService
    {
        private readonly ApplicationDbContext _context;

        public CommentaireVideoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentaireVideo> GetByIdAsync(int id)
        {
            return await _context.CommentairesVideo
                .Include(c => c.Video) // Include related video
                .Include(c => c.Utilisateur) // Include user who commented
                .Include(c => c.Reponses) // Include replies
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CommentaireVideo>> GetByVideoIdAsync(int videoId)
        {
            return await _context.CommentairesVideo
                .Where(c => c.VideoId == videoId)
                .Include(c => c.Utilisateur) // Include user who commented
                .Include(c => c.Reponses) // Include replies
                .ToListAsync();
        }

        public async Task<CommentaireVideo> CreateAsync(CommentaireVideo commentaire)
        {
            _context.CommentairesVideo.Add(commentaire);
            await _context.SaveChangesAsync();
            return commentaire;
        }

        public async Task<CommentaireVideo> UpdateAsync(CommentaireVideo commentaire)
        {
            _context.CommentairesVideo.Update(commentaire);
            await _context.SaveChangesAsync();
            return commentaire;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var commentaire = await _context.CommentairesVideo.FindAsync(id);
            if (commentaire != null)
            {
                _context.CommentairesVideo.Remove(commentaire);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
