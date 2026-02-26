using Lockhub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lockhub.Pages.Account
{
    public class Create_accountModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
            User = new User();
        }

        public IActionResult OnPost()
        {
            return Page();
        }

    }
}
