using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;
using static ELearningApp.Services.EtudiantService;

namespace ELearningApp.Services
{
    public class EnseignantService : IEnseignantService
    {
        private readonly ApplicationDbContext _context;

        public EnseignantService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int[]> GetDailyRegistrationsAsync()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // Get registrations for the current month
            var registrations = await _context.Enseignants
                .Where(e => e.joinDate.Year == currentYear && e.joinDate.Month == currentMonth)
                .GroupBy(e => (e.joinDate.Day - 1) / 3) // Group by 3-day intervals
                .Select(group => new { Interval = group.Key, Count = group.Count() })
                .ToListAsync();

            var intervalCounts = new int[10]; // 10 intervals for a maximum of 31 days

            foreach (var registration in registrations)
            {
                intervalCounts[registration.Interval] = registration.Count;
            }

            return intervalCounts;
        }
        public async Task<int[]> GetRegistrationsBy2HourIntervalsAsync()
        {
            var today = DateTime.Now.Date;

            var registrations = await _context.Enseignants
                .Where(e => e.joinDate.Date == today)
                .GroupBy(e => e.joinDate.Hour / 2) // Group by 2-hour intervals
                .Select(group => new { Interval = group.Key, Count = group.Count() })
                .ToListAsync();

            var intervals = new int[12]; // 12 intervals for 24 hours

            foreach (var registration in registrations)
            {
                intervals[registration.Interval] = registration.Count;
            }

            return intervals;
        }
        public async Task<int[]> GetMonthlyMentorsRegistrationsAsync()
        {
            var currentYear = DateTime.Now.Year;

            // Group students by month and count registrations
            var monthlyRegistrations = await _context.Enseignants
                .Where(e => e.joinDate.Year == currentYear) // Filter by the current year
                .GroupBy(e => e.joinDate.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    Count = group.Count()
                })
                .ToListAsync();

            // Initialize an array with 12 months set to 0
            var monthlyCounts = new int[12];

            // Fill the counts based on the months
            foreach (var registration in monthlyRegistrations)
            {
                monthlyCounts[registration.Month - 1] = registration.Count;
            }

            return monthlyCounts;
        }
        public async Task<PaginatedResult2<Enseignant>> GetPaginatedEnseignantsAsync(int pageNumber, int pageSize)
        {
            // Validate page size and page number
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            // Calculate the total count of students
            var totalCount = await _context.Enseignants.CountAsync();

            // Fetch the students for the requested page
            var items = await _context.Enseignants
                .OrderBy(e => e.FormalUserName) // Optional: Order by a column (adjust as needed)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Return the paginated result
            return new PaginatedResult2<Enseignant>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<List<Enseignant>> GetAllEnseignantsAsync()
        {
            return await _context.Enseignants.ToListAsync();
        }

        public async Task<Enseignant?> GetEnseignantByIdAsync(string enseignantId)
        {
            return await _context.Enseignants.FindAsync(enseignantId);
        }

        public async Task<Enseignant?> CreateEnseignantAsync(Enseignant enseignant, string password)
        {
            // Assuming password handling is managed separately
            await _context.Enseignants.AddAsync(enseignant);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? enseignant : null;
        }

        public async Task<bool> UpdateEnseignantAsync(Enseignant enseignant)
        {
            _context.Enseignants.Update(enseignant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEnseignantAsync(string enseignantId)
        {
            var enseignant = await _context.Enseignants.FindAsync(enseignantId);
            if (enseignant == null) return false;

            _context.Enseignants.Remove(enseignant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> BlockEnseignantAsync(string enseignantId)
        {
            var enseignant = await _context.Enseignants.FindAsync(enseignantId);
            if (enseignant == null) return false;

            enseignant.EmailConfirmed = !enseignant.EmailConfirmed;
            _context.Enseignants.Update(enseignant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Enseignant>> GetBlockedEnseignantsAsync()
        {
            return await _context.Enseignants.Where(e => !e.EmailConfirmed).ToListAsync();
        }

        public async Task<List<Enseignant>> GetActiveEnseignantsAsync()
        {
            return await _context.Enseignants.Where(e => e.EmailConfirmed).ToListAsync();
        }

        

        public class PaginatedResult2<T>
        {
            public List<T> Items { get; set; } = new();
            public int TotalCount { get; set; }
        }
    }
}
