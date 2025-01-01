using ELearningApp.Data;
using Microsoft.AspNetCore.Identity;

namespace ELearningApp.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetAuthenticatedUserAsync();
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string role);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string role);
    }
}
