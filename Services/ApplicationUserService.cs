using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scorekeeper.Models;

namespace Scorekeeper.Services
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // CREATE
        public void AddUser(ApplicationUser user, string password, List<string> roles)
        {
            var res = _userManager.CreateAsync(user, password).Result;
            if (res.Succeeded)
            {
                // Add default role if it is not already added
                if (!roles.Contains(ApplicationUser.DEFAULT_USER_ROLE))
                {
                    roles.Add(ApplicationUser.DEFAULT_USER_ROLE);
                }
                // Remove roles that do not exist
                foreach (var role in roles)
                {
                    if (!_roleManager.RoleExistsAsync(role).Result)
                    {
                       roles.Remove(role);
                    }
                }
                _userManager.AddToRolesAsync(user, roles).Wait();
            }
        }

        // READ
        public List<ApplicationUser> GetAllUsers()
        {
            if (_userManager.Users == null)
            {
                return new List<ApplicationUser>();
            }

            return _userManager.Users
                .Include(u => u.Scoreboards)
                .ToList();
        }

        public Dictionary<string, List<string>> GetAllUserRoles()
        {
            Dictionary<string, List<string>> userRoles = new Dictionary<string, List<string>>();
            List<ApplicationUser> users = GetAllUsers();
            foreach (ApplicationUser user in users)
            {
                List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
                userRoles.Add(user.Id, roles);
            }

            return userRoles;
        }

        public ApplicationUser? GetUser(string id)
        {
              return _userManager.Users
                .Where(x => x.Id == id)
                .Include(user => user.Scoreboards)
                .FirstOrDefault();
        }

        public List<string> GetAllRoles()
        {
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();
            if (roles == null)
            {
                roles = new List<string>();
            }

            return roles;
        }

        // UPDATE
        public void UpdateUser(ApplicationUser user, List<string>? newRoles = null)
        {
            // Update user roles
            if (newRoles != null)
            {
                _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).Result).Wait();
                _userManager.AddToRolesAsync(user, newRoles).Wait();
            }

            _userManager.UpdateAsync(user).Wait();
        }

        public void UpdateUserAddScoreboard(ApplicationUser user, Scoreboard scoreboard)
        {
        }

        public void UpdateUserRemoveScoreboard(ApplicationUser user, Scoreboard scoreboard)
        {
        }

        // DELETE
        public void DeleteUser(ApplicationUser user)
        {
            _userManager.DeleteAsync(user).Wait();
        }
        
        public void DeleteUser(string id)
        {
            ApplicationUser? user = GetUser(id);
            if (user != null)
            {
               DeleteUser(user);
            }
        }

    }
}
