using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Scorekeeper.Models;
using Scorekeeper.Services;
using System.Security.Claims;

namespace Scorekeeper.Pages
{
    public class IndexModel : PageModel
    {
        public const int SCOREBOARDS_PER_ROW = 3;
        public const int MAX_TEAMS_PER_THUMBNAIL = 4;

        private readonly ILogger<IndexModel> _logger;
        private readonly ScoreboardService _scoreboardService;
        
        public List<Scoreboard> Scoreboards { get; set; }

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
    }
}