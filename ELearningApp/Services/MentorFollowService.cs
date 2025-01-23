using ELearningApp.Data;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class MentorFollowService : IMentorFollowService
    {
        private readonly ApplicationDbContext _context;

        public MentorFollowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MentorFollow>> GetAllFollowsAsync()
        {
            return await _context.MentorFollows.ToListAsync();
        }

        public async Task<IEnumerable<MentorFollow>> GetFollowersByMentorIdAsync(string mentorId)
        {
            return await _context.MentorFollows.Where(f => f.EnseignantId == mentorId).ToListAsync();
        }

        public async Task<IEnumerable<MentorFollow>> GetMentorsFollowedByEtudiantAsync(string etudiantId)
        {
            return await _context.MentorFollows.Where(f => f.EtudiantId == etudiantId).ToListAsync();
        }

        public async Task<bool> IsFollowingAsync(string etudiantId, string mentorId)
        {
            return await _context.MentorFollows
                .AnyAsync(f => f.EtudiantId == etudiantId && f.EnseignantId == mentorId);
        }
        

        public async Task ToggleFollowMentorAsync(string etudiantId, string mentorId)
        {
            var follow = await _context.MentorFollows
                .FirstOrDefaultAsync(f => f.EtudiantId == etudiantId && f.EnseignantId == mentorId);
            if (follow != null)
            {
                // If the student is already following the mentor, unfollow
                _context.MentorFollows.Remove(follow);
            }
            else
            {
                // If the student is not following the mentor, follow
                _context.MentorFollows.Add(new MentorFollow
                {
                    EtudiantId = etudiantId,
                    EnseignantId = mentorId
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Error during database update: {dbEx.Message}");
                foreach (var entry in dbEx.Entries)
                {
                    Console.WriteLine($"Entity: {entry.Entity}, State: {entry.State}");
                }
            }

        }
    }
}
