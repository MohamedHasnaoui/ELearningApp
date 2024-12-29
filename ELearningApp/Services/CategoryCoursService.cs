using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class CategoryCoursService : ICategoryCoursService
    {
        private readonly ApplicationDbContext _context;

        public CategoryCoursService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryCours>> GetAllCategoriesAsync()
        {
            return await _context.CategoriesCours.Include(c => c.Courses).ToListAsync();
        }

        public async Task<CategoryCours> GetCategoryByIdAsync(int id)
        {
            return await _context.CategoriesCours.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CategoryCours> AddCategoryAsync(CategoryCours category)
        {
            _context.CategoriesCours.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryCours> UpdateCategoryAsync(CategoryCours category)
        {
            
            _context.CategoriesCours.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.CategoriesCours.FindAsync(id);
            if (category == null) return false;

            _context.CategoriesCours.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
