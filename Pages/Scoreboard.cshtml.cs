using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Scorekeeper.Models;
using Scorekeeper.Services;

namespace Scorekeeper.Pages
{
    public class ScoreboardModel : PageModel
    {
        private ScoreboardService _scoreboardService;
        private TeamService _teamService;

        public Scoreboard? Scoreboard { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? ScoreboardId { get; set; }
        [BindProperty]
        public float NewScore { get; set; }

        [ActivatorUtilitiesConstructor]
        public ScoreboardModel(ScoreboardService scoreboardService, TeamService teamService)
        {
            _scoreboardService = scoreboardService;
            _teamService = teamService;
        }

        public void OnGet()
        {
            if (ScoreboardId != null)
            {
                Scoreboard = _scoreboardService.GetScoreboard(ScoreboardId);
                if (Scoreboard == null)
                {
                    // Scoreboard not found
                    RedirectToPage("/404");
                }
            }
            else
            {
                // No ID; Bad Request
                RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostIncrementScore(string teamid, string scoreboardid)
        {
            changeScore(teamid, 1);
            return RedirectToAction("Get", new { ScoreboardId = scoreboardid });
        }
        public IActionResult OnPostDecrementScore(string teamid, string scoreboardid)
        {
            changeScore(teamid, -1);
            return RedirectToAction("Get", new { ScoreboardId = scoreboardid });
        }

        private void changeScore(string teamid, float delta)
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
