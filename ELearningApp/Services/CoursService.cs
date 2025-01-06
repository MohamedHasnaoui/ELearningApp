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
                .OrderByDescending(course => course.CreatedAt)
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
                .OrderByDescending(course => course.CreatedAt)
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
                .OrderByDescending(course => course.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Cours>> SearchCoursesByTitleAsync(string partialTitle, int pageNumber, int pageSize)
        {
 
            return await _context.Cours
                .Where(c => c.Nom.ToLower().Contains(partialTitle.ToLower()))
                .Include(c => c.Category)
                .OrderByDescending(course => course.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountCoursesByTitleAsync(string partialTitle)
        {

            return await _context.Cours
                .Where(c => c.Nom.ToLower().Contains(partialTitle.ToLower()))
                .CountAsync();
        }
        public async Task<List<Cours>> SearchCoursesByTitleAndCategoryIdAsync(string partialTitle, int categoryId, int pageNumber, int pageSize)
        {

            return await _context.Cours
                .Where(c => c.Nom.ToLower().Contains(partialTitle.ToLower()))
                .Where(c => c.CategoryId== categoryId)
                .Include(c => c.Category)
                .OrderByDescending(course => course.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountCoursesByTitleAndCategoryIdAsync(string partialTitle, int categoryId)
        {

            return await _context.Cours
                .Where(c => c.Nom.ToLower().Contains(partialTitle.ToLower()))
                .Where(c => c.CategoryId == categoryId)
                .CountAsync();
        }

        public async Task<Cours> CreateAsync(Cours cours)
        {
            cours.CreatedAt = DateTime.Now;
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
    }
}
