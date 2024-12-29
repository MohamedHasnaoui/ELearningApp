using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class CoursCommenceService : ICoursCommenceService
    {
        private readonly ApplicationDbContext _context;

        public CoursCommenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoursCommence>> GetAllAsync()
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Include(cc => cc.Etudiant)
                .ToListAsync();
        }

        public async Task<CoursCommence> GetByIdAsync(int id)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Include(cc => cc.Etudiant)
                .FirstOrDefaultAsync(cc => cc.Id == id);
        }

        public async Task<IEnumerable<CoursCommence>> GetByCoursIdAsync(int coursId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Where(cc => cc.CoursId == coursId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CoursCommence>> GetByEtudiantIdAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Where(cc => cc.EtudiantId == etudiantId)
                .ToListAsync();
        }
        public async Task<CoursCommence> GetByEtudiantAndCoursAsync(string etudiantId, int coursId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Include(cc => cc.Etudiant)
                .FirstOrDefaultAsync(cc => cc.EtudiantId == etudiantId && cc.CoursId == coursId);
        }
        public async Task<CoursCommence> AddAsync(CoursCommence coursCommence)
        {
            _context.CoursCommences.Add(coursCommence);
            await _context.SaveChangesAsync();
            return coursCommence;
        }

        public async Task<CoursCommence> UpdateAsync(CoursCommence coursCommence)
        {
            _context.CoursCommences.Update(coursCommence);
            await _context.SaveChangesAsync();
            return coursCommence;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coursCommence = await _context.CoursCommences.FindAsync(id);
            if (coursCommence == null) return false;

            _context.CoursCommences.Remove(coursCommence);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
