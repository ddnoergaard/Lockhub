using Lockhub.Models;
using Lockhub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lockhub.Pages.Account
{
    public class Create_accountModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string ComparePassword { get; set; }
        private IUserService _userService;
        [BindProperty]
        public string BirthDayString { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }


        public Create_accountModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            User = new User();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (User.HashPassword != ComparePassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords must match");
                return Page();
            }
            string[] tempArray = System.Text.RegularExpressions.Regex.Split(BirthDayString, ",|/|-");
            DateTime birthDate = new DateTime(Convert.ToInt32(tempArray[0]), Convert.ToInt32(tempArray[1]), Convert.ToInt32(tempArray[2]));
            User.BirthDate = birthDate;
            _userService.Register(User);
            return RedirectToPage("/Account/Login");
        }

    }
}
