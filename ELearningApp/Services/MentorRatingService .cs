using ELearningApp.Data;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class MentorRatingService : IMentorRatingService
    {
        private readonly ApplicationDbContext _context;

        public MentorRatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MentorRating>> GetAllRatingsAsync()
        {
            return await _context.MentorRatings.ToListAsync();
        }

        public async Task<MentorRating> GetRatingByIdAsync(string id)
        {
            return await _context.MentorRatings.FindAsync(id);
        }

        public async Task<IEnumerable<MentorRating>> GetRatingsByMentorIdAsync(string mentorId)
        {
            return await _context.MentorRatings.Where(r => r.EnseignantId == mentorId).ToListAsync();
        }

        public async Task<IEnumerable<MentorRating>> GetRatingsByEtudiantIdAsync(string etudiantId)
        {
            return await _context.MentorRatings.Where(r => r.EtudiantId == etudiantId).ToListAsync();
        }

        public async Task<double> GetAverageRatingForMentorAsync(string mentorId)
        {
            var ratings = await _context.MentorRatings.Where(r => r.EnseignantId == mentorId).ToListAsync();
            return ratings.Any() ? ratings.Average(r => r.Valeur) : 0;
        }

        public async Task AddRatingAsync(MentorRating rating)
        {
            _context.MentorRatings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(MentorRating rating)
        {
            _context.MentorRatings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(string id)
        {
            var rating = await _context.MentorRatings.FindAsync(id);
            if (rating != null)
            {
                _context.MentorRatings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
