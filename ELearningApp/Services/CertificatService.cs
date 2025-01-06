﻿using ELearningApp.Data;
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
