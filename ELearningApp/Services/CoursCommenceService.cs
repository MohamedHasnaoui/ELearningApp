﻿using ELearningApp.Data;
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
        public async Task<int[]> GetMonthlyCoursCommenceByEnseignantAsync(string enseignantId)
        {
            var currentYear = DateTime.Now.Year;

            // Group cours commencés by month and count for the specific enseignant
            var monthlyCoursCommences = await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Where(cc => cc.Cours.EnseignantId == enseignantId && cc.DateDebut.Year == currentYear)
                .GroupBy(cc => cc.DateDebut.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    Count = group.Count()
                })
                .ToListAsync();

            // Initialize an array with 12 months set to 0
            var monthlyCounts = new int[12];

            // Fill the counts based on the months
            foreach (var coursCommence in monthlyCoursCommences)
            {
                monthlyCounts[coursCommence.Month - 1] = coursCommence.Count;
            }

            return monthlyCounts;
        }

        public async Task<IEnumerable<CoursCommence>> GetAllAsync()
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Include(cc => cc.Etudiant)
                .ToListAsync();
        }

        public async Task<CoursCommence?> GetByIdAsync(int id)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .ThenInclude(c=>c.sections)
                .ThenInclude(s => s.Videos)
                .Include(cc => cc.Cours)
                .ThenInclude(c=>c.Enseignant)
                .Include(cc => cc.Etudiant)
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Examen)
                .OrderByDescending(c=>c.DateDebut)
                .FirstOrDefaultAsync(cc => cc.Id == id);
        }

        public async Task<IEnumerable<CoursCommence>> GetByCoursIdAsync(int coursId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .Where(cc => cc.CoursId == coursId)
                .ToListAsync();
        }
        public async Task<int> CountByCoursIdAsync(int coursId)
        {
            return await _context.CoursCommences
                .Where(cc => cc.CoursId == coursId)
                .CountAsync();
        }
        public async Task<IEnumerable<CoursCommence>> GetByEtudiantIdAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Enseignant)
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Category)
                .Where(cc => cc.EtudiantId == etudiantId)
                .OrderByDescending(c => c.DateDebut)
                .ToListAsync();
        }
        public async Task<IEnumerable<CoursCommence>> GetByEtudiantIdCompletedAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Enseignant)
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Category)
                .Where(cc => cc.EtudiantId == etudiantId)
                .Where(cc=>cc.Progres==100)
                .ToListAsync();
        }
        public async Task<IEnumerable<CoursCommence>> GetByEtudiantIdOnGoingAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Enseignant)
                .Include(cc => cc.Cours)
                .ThenInclude(c => c.Category)
                .Where(cc => cc.EtudiantId == etudiantId)
                .Where(cc => cc.Progres < 100)
                .ToListAsync();
        }
        public async Task<int> CountByEtudiantIdOnGoingAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Where(cc => cc.EtudiantId == etudiantId)
                .Where(cc => cc.Progres < 100)
                .CountAsync();
        }
        public async Task<int> CountByEtudiantIdCompletedAsync(string etudiantId)
        {
            return await _context.CoursCommences
                .Where(cc => cc.EtudiantId == etudiantId)
                .Where(cc => cc.Progres == 100)
                .CountAsync();
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
        public async Task<Dictionary<int, int>> GetCoursTermineParMoisAsync(int annee,string EtudiantId)
        {
            var result = await _context.CoursCommences
                .Where(cc => cc.DateFin.HasValue && cc.DateFin.Value.Year == annee && cc.EtudiantId==EtudiantId)
                .GroupBy(cc => cc.DateFin.Value.Month) // Grouper par mois
                .Select(g => new
                {
                    Mois = g.Key, 
                    Count = g.Count()
                })
                .ToListAsync();

            var fullResult = Enumerable.Range(1, 12)
            .ToDictionary(
                mois => mois, // Clé : le numéro du mois
                mois => result.FirstOrDefault(r => r.Mois == mois)?.Count ?? 0 // Valeur : 0 si aucune donnée
            );
            return fullResult;
        }


    }
}
