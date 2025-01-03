using CloudinaryDotNet;
using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace ELearningApp.Services
{
    public class CoursService : ICoursService
    {
        private readonly ApplicationDbContext _context;

        public CoursService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cours?> GetByIdAsync(int id)
        {
            return await _context.Cours
                .Include(c => c.Category) // Eagerly load the associated Category
                .Include(c => c.Enseignant)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cours>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Cours
                .Include(c => c.Category) 
                .Include(c => c.Enseignant)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cours>> GetCoursByCategoryIdAsync(int categoryId, int pageNumber, int pageSize)
        {
            return await _context.Cours
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Category) 
                .Include(c => c.Enseignant)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // New method to get Cours by EnseignantId
        public async Task<IEnumerable<Cours>> GetCoursByEnseignantIdAsync(string enseignantId, int pageNumber, int pageSize)
        {
            return await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filter by EnseignantId (CreateurId)
                .Include(c => c.Category)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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
        public async Task<int> CountCourses()
        {
            return await _context.Cours.CountAsync();
        }
        public async Task<int> CountByEnseignantId(string enseignantId)
        {
            return await _context.Cours.Where(c => c.EnseignantId == enseignantId).CountAsync();
        }
        public async Task<int> CountByCategoryIdAsync(int categoryId)
        {
            return await _context.Cours
                .Where(c => c.CategoryId == categoryId)
                .CountAsync();
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
