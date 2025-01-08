using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class CertificatService : ICertificatService
    {
        private readonly ApplicationDbContext _context;

        public CertificatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int[]> GetCertificatsIssuedLast10DaysAsync()
        {
            var today = DateTime.Now.Date;
            var last10Days = Enumerable.Range(0, 10)
                .Select(i => today.AddDays(-i)) // Get the last 10 days
                .ToList();

            var certificatCounts = await _context.Certificats
                .Where(c => c.DateObtention >= today.AddDays(-10)) // Filter certificates created in the last 10 days
                .GroupBy(c => c.DateObtention.Date) // Group certificates by their creation date
                .Select(g => new { CreatedDate = g.Key, Count = g.Count() })
                .ToListAsync();

            var result = new int[10];

            foreach (var day in last10Days)
            {
                var countForDay = certificatCounts.FirstOrDefault(c => c.CreatedDate == day)?.Count ?? 0;
                result[last10Days.IndexOf(day)] = countForDay;
            }

            return result;
        }
        public async Task<IEnumerable<Certificat>> GetAllAsync()
        {
            return await _context.Certificats
                .Include(c => c.Cours)
                .Include(c => c.Etudiant)
                .ToListAsync();
        }

        public async Task<Certificat?> GetByIdAsync(int id)
        {
            return await _context.Certificats
                .Include(c => c.Cours)
                .Include(c => c.Etudiant)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Certificat>> GetByEtudiantIdAsync(string etudiantId)
        {
            return await _context.Certificats
                .Include(c => c.Cours)
                .Where(c => c.EtudiantId == etudiantId)
                .ToListAsync();
        }
        public async Task<Certificat?> GetByEtudiantIdCoursIdAsync(string etudiantId, int coursId)
        {
            return await _context.Certificats
                .Include(c => c.Cours)
                .Where(c => c.EtudiantId == etudiantId)
                .Where(c=>c.CoursId==coursId)
                .FirstOrDefaultAsync();
        }

        public async Task<Certificat> AddAsync(Certificat certificat)
        {
            _context.Certificats.Add(certificat);
            await _context.SaveChangesAsync();
            return certificat;
        }

        public async Task<Certificat> UpdateAsync(Certificat certificat)
        {
            _context.Certificats.Update(certificat);
            await _context.SaveChangesAsync();

            return certificat;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificat = await _context.Certificats.FindAsync(id);
            if (certificat == null)
            {
                return false;
            }

            _context.Certificats.Remove(certificat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
