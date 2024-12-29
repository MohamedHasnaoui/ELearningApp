using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class ChoixService : IChoixService
    {
        private readonly ApplicationDbContext _context;

        public ChoixService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Choix> GetByIdAsync(int id)
        {
            return await _context.Choix
                .Include(c => c.Question) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Choix>> GetAllAsync()
        {
            return await _context.Choix
                .Include(c => c.Question) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Choix>> GetByQuestionIdAsync(int questionId)
        {
            return await _context.Choix
                .Where(c => c.QuestionId == questionId)
                .Include(c => c.Question)
                .ToListAsync();
        }

        public async Task<Choix> CreateAsync(Choix choix)
        {
            _context.Choix.Add(choix);
            await _context.SaveChangesAsync();
            return choix;
        }

        public async Task<Choix> UpdateAsync(Choix choix)
        {
            _context.Choix.Update(choix);
            await _context.SaveChangesAsync();
            return choix;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var choix = await _context.Choix.FindAsync(id);
            if (choix != null)
            {
                _context.Choix.Remove(choix);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
