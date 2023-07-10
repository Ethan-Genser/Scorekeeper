using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Scorekeeper.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ScoreboardService _scoreboardService;
        private readonly ApplicationUserService _userService;

        [BindProperty]
        public string? NewName { get; set; }
        [BindProperty]
        public List<string> NewUsernames { get; set; } = new List<string>();

        public List<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();

        public IndexModel(ILogger<IndexModel> logger, ScoreboardService scoreboardService, ApplicationUserService userService)
        {
            _logger = logger;
            _scoreboardService = scoreboardService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Claim? claim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                {
                    string userId = claim.Value;
                    Scoreboards = _scoreboardService.GetScoreboardsByUser(userId);
                }
            }
            return Page();
        }

        public IActionResult OnPostRedirectToScoreboard(string id)
        {
            return RedirectToPage("/scoreboard", new { ScoreboardId = id});
        }

        public IActionResult OnPostCreate()
        {
            // Set the name
            Scoreboard scoreboard = new Scoreboard();
            if (NewName != null)
            {
                scoreboard.Name = NewName;
            }

            // Assign the creator as the owner
            ApplicationUser? user = _userService.GetUser(User);
            if (user != null)
            {
                scoreboard.OwnerId = user.Id;
                scoreboard.Users.Add(user);
            }
            else
            {
                return LocalRedirect("/Error");
            }

            // Add each of the new users to the scoreboard
            foreach(string username in NewUsernames)
            {
                ApplicationUser? newuser = _userService.GetUserByUsername(username);
                if (newuser != null)
                {
                    scoreboard.Users.Add(newuser);
                }
            }

            _scoreboardService.AddScoreboard(scoreboard);
            return RedirectToAction("Get");
        }
    }
}