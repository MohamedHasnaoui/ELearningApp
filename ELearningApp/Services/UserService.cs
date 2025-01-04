using ELearningApp.Components.Account;
using ELearningApp.Data;
using ELearningApp.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private ApplicationUser? _cachedUser;

        public UserService(UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _userManager = userManager;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<ApplicationUser?> GetAuthenticatedUserAsync()
        {
            if (_cachedUser != null) return _cachedUser;

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var principal = authState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                _cachedUser = await _userManager.GetUserAsync(principal);
            }

            return _cachedUser;
        }


        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user != null ? await _userManager.DeleteAsync(user) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user != null ? await _userManager.GetRolesAsync(user) : new List<string>();
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string role)
        {
            var user = await GetUserByIdAsync(userId);
            return user != null ? await _userManager.AddToRoleAsync(user, role) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string role)
        {
            var user = await GetUserByIdAsync(userId);
            return user != null ? await _userManager.RemoveFromRoleAsync(user, role) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }
    }
}
