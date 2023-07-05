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

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public Scoreboard? Scoreboard { get; set; }

        [ActivatorUtilitiesConstructor]
        public ScoreboardModel(ScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        public void OnGet()
        {
            Scoreboard = _scoreboardService.GetScoreboard(Id);
            if (Scoreboard == null)
            {
                RedirectToPage("/404");
            }
        }
    }
}
