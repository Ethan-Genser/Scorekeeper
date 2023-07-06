using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;

namespace Scorekeeper.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ScoreboardService _scoreboardService;

        public List<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();

        public IndexModel(ILogger<IndexModel> logger, ScoreboardService scoreboardService)
        {
            _logger = logger;
            _scoreboardService = scoreboardService;
        }

        public void OnGet()
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
        }

        public IActionResult OnPostRedirectToScoreboard(string id)
        {
            return RedirectToPage("/scoreboard", new { ScoreboardId = id});
        }
    }
}