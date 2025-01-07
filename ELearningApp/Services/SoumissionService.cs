using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class SoumissionService : ISoumissionService
    {
        private readonly ApplicationDbContext _context;

        public SoumissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Soumission>> GetAllAsync()
        {
            return await _context.Soumissions
                .Include(s => s.Examen)
                .Include(s => s.Etudiant)
                .ToListAsync();
        }

        public async Task<Soumission> GetByIdAsync(int id)
        {
            return await _context.Soumissions
                .Include(s => s.Examen)
                .Include(s => s.Etudiant)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Soumission>> GetByEtudiantIdAsync(string etudiantId)
        {
            return await _context.Soumissions
                .Include(s => s.Examen)
                .Where(s => s.EtudiantId == etudiantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Soumission>> GetByExamenIdAsync(int assignementId)
        {
            return await _context.Soumissions
                .Include(s => s.Etudiant)
                .Where(s => s.ExamenId == assignementId)
                .OrderByDescending(s => s.DateSoumission) 
                .ToListAsync();
        }
        public async Task<List<Soumission>> GetByEtudiantAndExamenIdAsync(string etudiantId, int examId)
        {
            return await _context.Soumissions
                .Include(s => s.Examen)
                .Where(s => s.EtudiantId == etudiantId && s.ExamenId == examId)
                .OrderByDescending(s => s.DateSoumission)
                .Take(15)
                .ToListAsync();
        }
        public async Task<Soumission> AddAsync(Soumission soumission)
        {
            _context.Soumissions.Add(soumission);
            await _context.SaveChangesAsync();
            return soumission;
        }

        public async Task<Soumission> UpdateAsync(Soumission soumission)
        {
           
            _context.Soumissions.Update(soumission);
            await _context.SaveChangesAsync();

            return soumission;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var soumission = await _context.Soumissions.FindAsync(id);
            if (soumission == null)
            {
                return false;
            }

            _context.Soumissions.Remove(soumission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
