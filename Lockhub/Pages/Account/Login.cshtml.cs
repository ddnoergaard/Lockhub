using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lockhub.Pages.Account
{
    public class LoginModel : PageModel
    {
        private IUserService _userService;
        private IUserRepo _userRepo;
        [BindProperty]
        public User LoginUser { get; set; }

        public LoginModel(IUserService userService, IUserRepo userRepo)
        {
            _userService = userService;
            _userRepo = userRepo;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool success = await _userService.LoginAsync(LoginUser, Response);
            if (success)
            {
                return RedirectToPage("/App/Dashboard");
            }
            ViewData["failed-to-login"] = "Wrong credentials. Try again.";
            return Page();
        }

    }
}
