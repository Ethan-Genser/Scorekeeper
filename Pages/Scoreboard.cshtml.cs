using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.Net;

namespace Scorekeeper.Pages
{
    [Authorize(Roles = "User")]
    public class ScoreboardModel : PageModel
    {
        private ScoreboardService _scoreboardService;
        private TeamService _teamService;
        private ApplicationUserService _userService;

        public Scoreboard? Scoreboard { get; set; }
        public ApplicationUser? CurrentUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ScoreboardId { get; set; }
        [BindProperty]
        public string? NewName { get; set; }
        [BindProperty]
        public string? NewColor { get; set; }
        [BindProperty]
        public float? NewScore { get; set; }

        [ActivatorUtilitiesConstructor]
        public ScoreboardModel(ScoreboardService scoreboardService, TeamService teamService, ApplicationUserService userService)
        {
            _scoreboardService = scoreboardService;
            _teamService = teamService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            CurrentUser = _userService.GetUser(User);

            if (ScoreboardId != null)
            {
                Scoreboard = _scoreboardService.GetScoreboard(ScoreboardId);
                if (Scoreboard == null)
                {
                    // Scoreboard not found
                    return LocalRedirect("/404");
                }
            }
            else
            {
                // No ID; Bad Request
                return LocalRedirect("/Error");
            }

            if (!Scoreboard.Users.Contains(_userService.GetUser(User)))
            {
                // User not authorized to view this scoreboard
                return LocalRedirect("/Error");
            }

            return Page();
        }

        public IActionResult OnPostIncrementScore(string teamid, string scoreboardid)
        {
            ChangeScore(teamid, 1);
            return RedirectToAction("Get", new { ScoreboardId = scoreboardid });
        }
        public IActionResult OnPostDecrementScore(string teamid, string scoreboardid)
        {
            ChangeScore(teamid, -1);
            return RedirectToAction("Get", new { ScoreboardId = scoreboardid });
        }
        public IActionResult OnPostCreate(string scoreboardid)
        {
            Team team = new Team();
            if (NewName != null)
            {
                team.Name = NewName;
            }
            if (NewColor != null)
            {
                try
                {
                    team.Color = (int)Enum.Parse<Team.ColorName>(NewColor);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
            }

            Scoreboard? scoreboard = _scoreboardService.GetScoreboard(scoreboardid);
            if (scoreboard != null)
            {
                team.Scoreboard = scoreboard;
            }
            else
            {
                return NotFound();
            }

            _teamService.AddTeam(team);

            return RedirectToAction("Get", new { ScoreboardId = scoreboardid });
        }

        private void ChangeScore(string teamid, float delta)
        {
            var team = _teamService.GetTeam(teamid);
            if (team != null)
            {
                team.Score += delta;
                _teamService.UpdateTeam(team);
            }
        }
    }
}
