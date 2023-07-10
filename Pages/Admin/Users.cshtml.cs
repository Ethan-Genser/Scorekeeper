using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Scorekeeper.Pages.Admin
{
    
    public class UsersModel : PageModel
    {
        private readonly ApplicationUserService _userService;

        [BindProperty]
        public string? NewId { get; set; } = default!;

        [BindProperty]
        public string? NewUserName { get; set; } = default!;

        [Required]
        [BindProperty]
        [EmailAddress]
        public string NewEmail { get; set; } = default!;

        [Required]
        [BindProperty]
        public bool NewEmailConfirmed { get; set; } = true;

        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = default!;

        [BindProperty]
        public string? NewRoles { get; set; } = default!;

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public Dictionary<string, List<string>> UserRoles { get; set; } = new Dictionary<string, List<string>>();

        [ActivatorUtilitiesConstructor]
        public UsersModel(ApplicationUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            Users = _userService.GetAllUsers();
            UserRoles = _userService.GetAllUserRoles();
            return Page();
        }

        // CREATE
        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                var user = Activator.CreateInstance<ApplicationUser>();
                user.Id = NewId ?? Guid.NewGuid().ToString();
                user.UserName = NewUserName ?? NewEmail.Split("@")[0];
                user.Email = NewEmail;
                user.EmailConfirmed = NewEmailConfirmed;

                List<string> roles = NewRoles?.Replace(" ", "").Split(",").ToList() ?? new List<string>();

                _userService.AddUser(user, NewPassword, roles);

                return RedirectToAction("Get");
            }
            return Page();
        }

        // UPDATE
        public IActionResult OnPostUpdate(string id)
        {
            ModelState.Remove("NewPassword");
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = NewUserName ?? user.UserName;
                user.Email = NewEmail;
                user.EmailConfirmed = NewEmailConfirmed;
                List<string> roles = NewRoles?.Split(", ").ToList() ?? new List<string>();

                _userService.UpdateUser(user, roles);

                return RedirectToAction("Get");
            }
            return Page();
        }

        // DELETE
        public IActionResult OnPostDelete(string id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Get");
        }
    }
}
