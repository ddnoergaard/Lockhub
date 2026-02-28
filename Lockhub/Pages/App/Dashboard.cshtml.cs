using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lockhub.Pages.App
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private IUserRepo _userRepo;
        public User CurrentUser { get; set; }

        public DashboardModel(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void OnGet()
        {
            int userId = Convert.ToInt32(User.FindFirst("userId").Value);
            CurrentUser = _userRepo.GetUserById(userId);
        }
    }
}
