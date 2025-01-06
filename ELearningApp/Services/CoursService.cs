﻿using CloudinaryDotNet;
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
            var cours = await _context.Cours
                .Include(c => c.sections)  // Load associated sections
                .ThenInclude(s => s.Videos)
                .ThenInclude(v=>v.Commentaires)// Load associated videos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cours != null)
            {
                foreach (var section in cours.sections)
                {
                    foreach (var video in section.Videos)
                    {
                        foreach(var comment in video.Commentaires)
                        {
                            _context.CommentairesVideo.Remove(comment);
                        }
                    }
                }

                foreach (var section in cours.sections)
                {
                    foreach (var video in section.Videos)
                    {
                        _context.Videos.Remove(video);
                    }
                }

                foreach (var section in cours.sections)
                {
                    _context.Sections.Remove(section);
                }

                _context.Cours.Remove(cours);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }


        public async Task<List<EtudiantCoursInfo>> GetEtudiantsInscritsAsync(string enseignantId)
        {
            return await _context.CoursCommences
                .Where(cc => cc.Cours.EnseignantId == enseignantId)
                .OrderByDescending(cc => cc.DateDebut)  // Trie les étudiants par date d'inscription, du plus récent au plus ancien
                .Select(cc => new EtudiantCoursInfo
                {
                    EtudiantId = cc.EtudiantId,
                    NomCours = cc.Cours.Nom,
                    DateInscription = cc.DateDebut,
                    Statut = cc.Progres == 100 ? "Completé" : "En cours",
                    ImgProfile = cc.Etudiant.imgProfile,
                    NomEtudiant = cc.Etudiant.UserName // Récupération du nom de l'étudiant
                })
                .ToListAsync();
        }

        public async Task<List<TopCoursDto>> GetTop5CoursByEnseignantAsync(string enseignantId)
        {
            var topCours = await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filtrer par enseignant
                .Include(c => c.Enseignant) // Inclure les informations sur l'enseignant
                .Select(c => new TopCoursDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Description = c.Description,
                    Evaluation = c.Evaluation,
                    NombreEtudiants = _context.CoursCommences.Count(cc => cc.CoursId == c.Id),
                    EnseignantNom = c.Enseignant.UserName // Nom de l'enseignant
                })
                .OrderByDescending(c => c.Evaluation) // Trier par évaluation
                .ThenByDescending(c => c.NombreEtudiants) // Puis par nombre d'étudiants
                .Take(5) // Prendre les 5 premiers
                .ToListAsync();

            return topCours;
        }


    }
    public class EtudiantCoursInfo
    {
        public string? EtudiantId { get; set; }
        public string? NomCours { get; set; }
        public DateTime DateInscription { get; set; }
        public string? Statut { get; set; }
        public string? ImgProfile { get; set; }
        public string? NomEtudiant { get; set; } // Nouvelle propriété
    }
    public class TopCoursDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Description { get; set; }
        public double Evaluation { get; set; }
        public int NombreEtudiants { get; set; }
        public string? EnseignantNom { get; set; }
    }

}
