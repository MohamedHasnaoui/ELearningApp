using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class CoursService : ICoursService
    {
        private readonly ApplicationDbContext _context;

        public CoursService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cours> GetByIdAsync(int id)
        {
            return await _context.Cours
                .Include(c => c.Category) // Eagerly load the associated Category
                .Include(c => c.Enseignant)
                .Include(c => c.Examen)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cours>> GetAllAsync()
        {
            return await _context.Cours
                .Include(c => c.Category) // Eagerly load the associated Category
                .Include(c => c.Enseignant) // Eagerly load the related Teacher (Enseignant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cours>> GetCoursByCategoryIdAsync(int categoryId)
        {
            return await _context.Cours
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Category) // Eagerly load the associated Category
                .Include(c => c.Enseignant) // Eagerly load the related Teacher (Enseignant)
                .ToListAsync();
        }

        // New method to get Cours by EnseignantId
        public async Task<IEnumerable<Cours>> GetCoursByEnseignantIdAsync(string enseignantId)
        {
            return await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filter by EnseignantId (CreateurId)
                .Include(c => c.Category) // Eagerly load the associated Category
                .ToListAsync();
        }

        public async Task<Cours> CreateAsync(Cours cours)
        {
            _context.Cours.Add(cours);
            await _context.SaveChangesAsync();
            return cours;
        }

        public async Task<Cours> UpdateAsync(Cours cours)
        {
            _context.Cours.Update(cours);
            await _context.SaveChangesAsync();
            return cours;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cours = await _context.Cours.FindAsync(id);
            if (cours != null)
            {
                _context.Cours.Remove(cours);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
