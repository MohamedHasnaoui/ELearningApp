using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class ReponseCommentaireService : IReponseCommentaireService
    {
        private readonly ApplicationDbContext _context;

        public ReponseCommentaireService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReponseCommentaire> GetByIdAsync(int id)
        {
            return await _context.ReponsesCommentaire
                .Include(r => r.Commentaire)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ReponseCommentaire>> GetByCommentaireIdAsync(int commentaireId)
        {
            return await _context.ReponsesCommentaire
                .Where(r => r.CommentaireId == commentaireId)
                .Include(r => r.Utilisateur)
                .ToListAsync();
        }

        public async Task<ReponseCommentaire> CreateAsync(ReponseCommentaire reponse)
        {
            _context.ReponsesCommentaire.Add(reponse);
            await _context.SaveChangesAsync();
            return reponse;
        }

        public async Task<ReponseCommentaire> UpdateAsync(ReponseCommentaire reponse)
        {
            _context.ReponsesCommentaire.Update(reponse);
            await _context.SaveChangesAsync();
            return reponse;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reponse = await _context.ReponsesCommentaire.FindAsync(id);
            if (reponse != null)
            {
                _context.ReponsesCommentaire.Remove(reponse);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
