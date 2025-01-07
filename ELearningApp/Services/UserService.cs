using ELearningApp.Components.Account;
using ELearningApp.Data;
using ELearningApp.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private ApplicationUser? _cachedUser;
        private readonly ApplicationDbContext _dbContext;


        public UserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _userManager = userManager;
            _authenticationStateProvider = authenticationStateProvider;
            _dbContext = dbContext;
        }
        public async Task<int[]> GetUsersJoinedLast10DaysAsync()
        {
            var today = DateTime.Now.Date;
            var last10Days = Enumerable.Range(0, 10)
                .Select(i => today.AddDays(-i)) // Get the last 10 days
                .ToList();

            var userCounts = await _userManager.Users
                .Where(u => u.joinDate >= today.AddDays(-10)) // Filter users created in the last 10 days
                .GroupBy(u => u.joinDate.Date) // Group users by their join date
                .Select(g => new { JoinDate = g.Key, Count = g.Count() })
                .ToListAsync();

            var result = new int[10];

            foreach (var day in last10Days)
            {
                var countForDay = userCounts.FirstOrDefault(u => u.JoinDate == day)?.Count ?? 0;
                result[last10Days.IndexOf(day)] = countForDay;
            }

            return result;
        }
        
        public async Task<ApplicationUser?> GetAuthenticatedUserAsync()
        {
            if (_cachedUser != null) return _cachedUser;

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    _cachedUser = await _dbContext.Users.FindAsync(userId);
                }
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
