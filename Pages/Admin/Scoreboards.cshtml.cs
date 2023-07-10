using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.Data;

namespace Scorekeeper.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ScoreboardsModel : PageModel
    {
        private readonly ScoreboardService _scoreboardService;
        private readonly ApplicationUserService _userService;


        public List<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>(); 

        [BindProperty]
        public string? NewName { get; set; } = default!;
        [BindProperty]
        public string? NewColor { get; set; } = default!;
        [BindProperty]
        public string? NewOwnerId { get; set; } = default!;
        [BindProperty]
        public string? NewUsers { get; set; } = default!;


        [ActivatorUtilitiesConstructor]
        public ScoreboardsModel(ScoreboardService scoreboardService, ApplicationUserService userService)
        {
            _scoreboardService = scoreboardService;
            _userService = userService;
        }


        public IActionResult OnGet()
        {
            Scoreboards = _scoreboardService.GetAllScoreboards();
            return Page();
        }

        public IActionResult OnPostCreate()
        {
            Scoreboard scoreboard = new Scoreboard();
            if (NewName != null)
            {
                scoreboard.Name = NewName;
            }
            if (NewColor != null)
            {
                scoreboard.Color = NewColor;
            }
            if (NewOwnerId != null)
            {
                scoreboard.OwnerId = NewOwnerId;
            }


            List<string> userIds = NewUsers?.Replace(" ", "").Split(",").ToList() ?? new List<string>();
            foreach (var userId in userIds)
            {
                ApplicationUser? user = _userService.GetUser(userId);
                if (user != null)
                {
                    scoreboard.Users.Add(user);
                }
                else // If any of the provided users are not found cancel the whole operation
                {
                    return NotFound();
                }
            }

            _scoreboardService.AddScoreboard(scoreboard);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostUpdate(string id)
        {
            Scoreboard? scoreboard = _scoreboardService.GetScoreboard(id);
            if (scoreboard == null)
            {
                return NotFound();
            }

            if (NewName != null)
            {
                scoreboard.Name = NewName;
            }
            if (NewColor != null)
            {
                scoreboard.Color = NewColor;
            }
            if (NewOwnerId != null)
            {
                scoreboard.OwnerId = NewOwnerId;
            }


            scoreboard.Users = new List<ApplicationUser>();
            List<string> userIds = NewUsers?.Replace(" ", "").Split(",").ToList() ?? new List<string>();
            foreach (var userId in userIds)
            {
                ApplicationUser? user = _userService.GetUser(userId);
                if (user != null)
                {
                    scoreboard.Users.Add(user);
                }
                else // If any of the provided users are not found cancel the whole operation
                {
                    return NotFound();
                }
            }

            _scoreboardService.UpdateScoreboard(scoreboard);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(string id)
        {
            _scoreboardService.DeleteScoreboard(id);
            return RedirectToAction("Get");
        }
    }
}
