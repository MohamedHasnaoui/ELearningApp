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
        private readonly HttpClient _httpClient;


        public CoursService(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;

        }
        public async Task<int[]> GetCoursesCreatedLast10DaysAsync()
        {
            var today = DateTime.Now.Date;
            var last10Days = Enumerable.Range(0, 10)
                .Select(i => today.AddDays(-i)) // Get the last 10 days
                .ToList();

            var courseCounts = await _context.Cours
                .Where(c => c.CreatedAt >= today.AddDays(-10)) // Filter courses created in the last 10 days
                .GroupBy(c => c.CreatedAt.Date) // Group courses by their creation date
                .Select(g => new { CreatedDate = g.Key, Count = g.Count() })
                .ToListAsync();

            var result = new int[10];

            foreach (var day in last10Days)
            {
                var countForDay = courseCounts.FirstOrDefault(c => c.CreatedDate == day)?.Count ?? 0;
                result[last10Days.IndexOf(day)] = countForDay;
            }

            return result;
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
        public async Task<IEnumerable<Cours>> GetAllCoursByEnseignantAsync(string enseignantId)
        {
            return await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filter by EnseignantId (CreateurId)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<List<Cours>> GetTop3RatedCoursByEnseignantIdAsync(string enseignantId)
        {
            return await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filter by EnseignantId (CreateurId)
                .Include(c => c.Category)
                .OrderByDescending(course => course.Evaluation)
                .Take(3)
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

        public async Task<List<TopCoursDto>> GetTop5EvaluationCoursByEnseignantAsync(string enseignantId)
        {
            var topCours = await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filtrer par enseignant
                .Include(c => c.Enseignant) // Inclure les informations sur l'enseignant
                .Select(c => new TopCoursDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Category = c.Category.Name,
                    Evaluation = c.Evaluation,
                    NombreEtudiants = _context.CoursCommences.Count(cc => cc.CoursId == c.Id),
                    EnseignantNom = c.Enseignant.UserName // Nom de l'enseignant
                })
                .OrderByDescending(c => c.Evaluation) // Trier par évaluation
                .ThenByDescending(c => c.NombreEtudiants)
                .Take(5) // Prendre les 5 premiers
                .ToListAsync();

            return topCours;
        }
        public async Task<List<TopCoursDto>> GetTop5NbEtudinatCoursByEnseignantAsync(string enseignantId)
        {
            var topCours = await _context.Cours
                .Where(c => c.EnseignantId == enseignantId) // Filtrer par enseignant
                .Include(c => c.Enseignant) // Inclure les informations sur l'enseignant
                .Select(c => new TopCoursDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Evaluation = c.Evaluation,
                    NombreEtudiants = _context.CoursCommences.Count(cc => cc.CoursId == c.Id),
                    EnseignantNom = c.Enseignant.UserName // Nom de l'enseignant
                })
                .OrderByDescending(c => c.NombreEtudiants) // Trier par évaluation
                .ThenByDescending(c=>c.Evaluation)
                .Take(5) // Prendre les 5 premiers
                .ToListAsync();

            return topCours;
        }

        public async Task<List<CourseStats>> GetStudentEnrollmentsByTeacherAsync(string enseignantId)
        {
            // Récupérer l'année actuelle
            int currentYear = DateTime.Now.Year;

            // Filtrer les cours commencés pour l'enseignant donné et pour l'année actuelle
            var results = await _context.CoursCommences
                .Where(cc => cc.Cours.EnseignantId == enseignantId && cc.DateDebut.Year == currentYear)
                .GroupBy(cc => new
                {
                    Month = cc.DateDebut.Month,
                    Year = cc.DateDebut.Year
                })
                .Select(g => new CourseStats
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    // Assurez-vous de compter les étudiants uniques par mois et année
                    StudentCount = g.Select(cc => cc.EtudiantId).Distinct().Count()  // Utilisation de Distinct pour compter les étudiants uniques
                })
                .OrderBy(cs => cs.Month)  // Optionnel, pour trier par mois
                .ToListAsync();

            // Ajouter un log pour voir les résultats
            Console.WriteLine("Student Enrollments per Month for current year: " + string.Join(", ", results.Select(cs => $"{cs.Month}/{cs.Year} - {cs.StudentCount}")));

            return results;
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
        public string? Category { get; set; }
        public double Evaluation { get; set; }
        public int NombreEtudiants { get; set; }
        public string? EnseignantNom { get; set; }
    }
 

    public class CourseStats
    {
        public string CourseName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int StudentCount { get; set; }
        public string MonthName => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);

    }
}
