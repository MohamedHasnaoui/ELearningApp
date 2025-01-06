using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class ExamenService : IExamenService
    {
        private readonly ApplicationDbContext _context;

        public ExamenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Examen> GetByIdAsync(int id)
        {
            return await _context.Examens
                .Include(e => e.Cours) // Include the related Cours
                .Include(e => e.Questions) // Include related Questions
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Examen>> GetAllAsync()
        {
            return await _context.Examens
                .Include(e => e.Cours) // Include the related Cours
                .ToListAsync();
        }

        public async Task<Examen> GetByCoursIdAsync(int coursId)
        {
            return await _context.Examens
                .Where(e => e.CoursId == coursId)
                .Include(e => e.Questions)
                .ThenInclude(Q=>Q.Choix)
                .Include(e => e.Cours)
                .FirstAsync();
        }

        public async Task<Examen> CreateAsync(Examen examen)
        {
            _context.Examens.Add(examen);
            await _context.SaveChangesAsync();
            return examen;
        }

        public async Task<Examen> UpdateAsync(Examen examen)
        {
            _context.Examens.Update(examen);
            await _context.SaveChangesAsync();
            return examen;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var examen = await _context.Examens.FindAsync(id);
            if (examen != null)
            {
                _context.Examens.Remove(examen);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
