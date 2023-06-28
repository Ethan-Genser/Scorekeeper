using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scorekeeper.Models;
using Scorekeeper.Services;

namespace Scorekeeper.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class TeamsModel : PageModel
    {
        private readonly TeamService _teamService;
        private readonly ScoreboardService _scoreboardService;

        public List<Team> Teams { get; set; } = new List<Team>();

        [BindProperty]
        public string? NewName { get; set; } = default!;
        [BindProperty]
        public string? NewColor { get; set; } = default!;
        [BindProperty]
        public float? NewScore { get; set; } = default!;
        [BindProperty]
        public string NewScoreboardId { get; set; } = default!;

        public TeamsModel(TeamService teamService, ScoreboardService scoreboardService)
        {
            _teamService = teamService;
            _scoreboardService = scoreboardService;
        }

        public void OnGet()
        {
            Teams = _teamService.GetAllTeams();
        }

        public IActionResult OnPostCreate()
        {
            Team team = new Team();
            if (NewName != null)
            {
                team.Name = NewName;
            }
            if (NewColor != null)
            {
                team.Color = NewColor;
            }
            if (NewScore != null)
            {
                team.Score = (float)NewScore;
            }
            if (NewScoreboardId != null)
            {
                Scoreboard? scoreboard = _scoreboardService.GetScoreboard(NewScoreboardId);
                if (scoreboard != null)
                {
                    team.Scoreboard = scoreboard;
                }
                else
                {
                    return NotFound();
                }
            }

            _teamService.AddTeam(team);

            return RedirectToAction("Get");
        }   
    }
}