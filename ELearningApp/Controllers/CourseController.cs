using ELearningApp.Data;
using ELearningApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearningApp.Controllers
{

    [Route("api/courses")]
    [ApiController]

    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Route pour récupérer les statistiques des cours par enseignant
/*        [HttpGet("stats/{enseignantId}")]
*//*        public async Task<ActionResult<List<CourseStats>>> GetStudentEnrollmentsByTeacherAsync(string enseignantId)
        {
            var results = await _context.CoursCommences
                .Where(cc => cc.Cours.EnseignantId == enseignantId)
                .GroupBy(cc => new
                {
                    cc.Cours.Nom,
                    Month = cc.DateDebut.Month,
                    Year = cc.DateDebut.Year
                })
                .Select(g => new CourseStats
                {
                    CourseName = g.Key.Nom,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    StudentCount = g.Count()
                }).ToListAsync();

            if (results == null || !results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }
*/    }
}
