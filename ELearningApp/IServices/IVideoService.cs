using ELearningApp.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace ELearningApp.IServices
{
    public interface IVideoService
    {
        Task<Video> GetByIdAsync(int id);
        Task<IEnumerable<Video>> GetVideosBySectionIdAsync(int sectionId);
        Task<bool> CreateAsync(Video video);
        Task<Video> UpdateAsync(Video video);
        Task DeleteAsync(int id);

    }
}
