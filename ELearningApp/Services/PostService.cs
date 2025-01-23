using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public PostService(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;

        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.Enseignant) // Charger les infos de l'enseignant
                .ToListAsync();
        }


        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Post>> GetPostsByEnseignantIdAsync(string enseignantId)
        {
            return await _context.Posts
                .Where(p => p.EnseignantId == enseignantId)
                .Include(p => p.Enseignant) // Inclure les détails de l'enseignant
                .ToListAsync();
        }


        public async Task<Post> CreatePostAsync(Post post)
        {
           

           
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                throw new KeyNotFoundException($"Post with ID {id} not found.");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
