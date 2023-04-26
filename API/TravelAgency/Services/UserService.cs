using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirstValue("sub");
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return user;
                }
            }
            throw new InvalidOperationException();
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser user) => await _userManager.GetRolesAsync(user);
    }
}
