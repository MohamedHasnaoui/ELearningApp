using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class SectionService : ISectionService
    {
        private readonly ApplicationDbContext _context;

        public SectionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(Section section)
        {
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var section = await _context.Sections.Include(s=>s.Videos).ThenInclude(v=>v.Commentaires)
                .Where(s=>s.Id==id).FirstAsync();
            foreach (var video in section.Videos)
            {
                foreach (var comment in video.Commentaires)
                {
                    _context.CommentairesVideo.Remove(comment);
                }
            }
            foreach (var video in section.Videos)
            {
                _context.Videos.Remove(video);
            }
            _context.Sections.Remove(section);

            return true;

        }

        public async Task<Section> GetByIdAsync(int id)
        {
            return await _context.Sections
                .Include(s => s.Videos) 
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Section>> GetSectionsByCoursIdAsync(int coursId)
        {
            return await _context.Sections.AsNoTracking()
                .Where(s => s.CoursId == coursId).AsNoTracking()
                .Include(s => s.Videos).AsNoTracking() // Eagerly load related Videos
                .ToListAsync();
        }

        public async Task<Section> UpdateAsync(Section section)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync();
            return section;
        }
        public async Task<int> CountSectionsByCourseIdAsync(int coursId)
        {
            return await _context.Sections.CountAsync(s => s.CoursId == coursId);
        }

        // Implementation of the new method
        public async Task<bool> DeleteAllByCourseIdAsync(int coursId)
        {
            var sections = await _context.Sections.Where(s => s.CoursId == coursId).ToListAsync();
            if (sections.Any())
            {
                _context.Sections.RemoveRange(sections);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
