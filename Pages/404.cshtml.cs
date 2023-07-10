using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scorekeeper.Pages
{
    [AllowAnonymous]
    public class _404Model : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
