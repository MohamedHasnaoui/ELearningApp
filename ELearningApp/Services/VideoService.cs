using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class VideoService : IVideoService
    {
        ApplicationDbContext _context;
        public VideoService(ApplicationDbContext context) {
 
            _context = context;
        }

        public async Task<bool> CreateAsync(Video video)
        {
            try {
                _context.Videos.Add(video);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Video>> GetVideosBySectionIdAsync(int sectionId)
        {
            return await _context.Videos
                .Where(v => v.SectionId == sectionId)
                .ToListAsync();
        }

        public async Task<Video> GetByIdAsync(int id)
        {
            return await _context.Videos.Where(v=>v.Id==id)
                .Include(v=>v.Commentaires)
                .FirstAsync();
        }

        public async Task<Video> UpdateAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task DeleteAsync(int id)
        {
            var video = await GetByIdAsync(id);

            if (video == null)
            {
                return;
            }
            foreach (var comment in video.Commentaires)
            {
                _context.CommentairesVideo.Remove(comment);
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }

    }
}
