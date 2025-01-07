using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class EtudiantService : IEtudiantService
    {
        private readonly ApplicationDbContext _context;

        public EtudiantService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int[]> GetDailyRegistrationsAsync()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // Get registrations for the current month
            var registrations = await _context.Etudiants
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

            var registrations = await _context.Etudiants
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

        public async Task<int[]> GetMonthlyStudentRegistrationsAsync()
        {
            var currentYear = DateTime.Now.Year;

            // Group students by month and count registrations
            var monthlyRegistrations = await _context.Etudiants
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


        public async Task<PaginatedResult<Etudiant>> GetPaginatedEtudiantsAsync(int pageNumber, int pageSize)
        {
            // Validate page size and page number
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            // Calculate the total count of students
            var totalCount = await _context.Etudiants.CountAsync();

            // Fetch the students for the requested page
            var items = await _context.Etudiants
                .OrderBy(e => e.FormalUserName) // Optional: Order by a column (adjust as needed)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Return the paginated result
            return new PaginatedResult<Etudiant>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<List<Etudiant>> GetAllEtudiantsAsync()
        {
            return await _context.Etudiants.ToListAsync();
        }

        public async Task<Etudiant?> GetEtudiantByIdAsync(string etudiantId)
        {
            return await _context.Etudiants.FindAsync(etudiantId);
        }

        public async Task<Etudiant?> CreateEtudiantAsync(Etudiant etudiant, string password)
        {
            // Assuming password handling is not required directly with ApplicationDbContext
            await _context.Etudiants.AddAsync(etudiant);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? etudiant : null;
        }

        public async Task<bool> UpdateEtudiantAsync(Etudiant etudiant)
        {
            _context.Etudiants.Update(etudiant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEtudiantAsync(string etudiantId)
        {
            var etudiant = await _context.Etudiants.FindAsync(etudiantId);
            if (etudiant == null) return false;

            _context.Etudiants.Remove(etudiant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> BlockEtudiantAsync(string etudiantId)
        {
            var etudiant = await _context.Etudiants.FindAsync(etudiantId);
            if (etudiant == null) return false;

            etudiant.EmailConfirmed = !etudiant.EmailConfirmed;
            _context.Etudiants.Update(etudiant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Etudiant>> GetBlockedEtudiantsAsync()
        {
            return await _context.Etudiants.Where(e => !e.EmailConfirmed).ToListAsync();
        }

        public async Task<List<Etudiant>> GetActiveEtudiantsAsync()
        {
            return await _context.Etudiants.Where(e => e.EmailConfirmed).ToListAsync();
        }
        public class PaginatedResult<T>
        {
            public List<T> Items { get; set; } = new();
            public int TotalCount { get; set; }
        }

    }
}
