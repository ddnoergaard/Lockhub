using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services;
using Lockhub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lockhub.Pages.App
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private IUserRepo _userRepo;
        private ICredentialService _credentialService;
        private IVaultService _vaultService;
        private IUserService _userService;
        public User CurrentUser { get; set; }
        public int StoredPasswordAmount { get; set; }
        public int VaultsAccessTo { get; set; }
        public int WeakPasswords { get; set; }
        public string TimeOfDayMessage { get; set; }
        public List<Credential> credentials;
        public DateTime UserLastLogin { get; set; }
        public int CredentialCount { get; set; }
        public int VaultCount { get; set; }
        public int WeakPasswordCount { get; set; }

        public DashboardModel(IUserRepo userRepo, ICredentialService credentialService, IVaultService vaultService, IUserService userService)
        {
            _userRepo = userRepo;
            _credentialService = credentialService;
            _vaultService = vaultService;
            _userService = userService;
        }

        private string CalculateTimeOfDayMessage()
        {
            int hour = DateTime.Now.Hour;
            if (hour > 5 && hour < 12)
            {
                return "Good morning";
            } else if (hour > 12 && hour < 18)
            {
                return "Good afternoon";
            } else if (hour > 18 && hour < 22)
            {
                return "Good evening";
            } else if (hour > 22)
            {
                return "Good night";
            } else
            {
                return "Welcome";
            }
        }

        public void OnGet()
        {
            int userId = Convert.ToInt32(User.FindFirst("userId").Value);
            CurrentUser = _userRepo.GetUserById(userId);
            UserLastLogin = CurrentUser.LastLogin;
            TimeOfDayMessage = CalculateTimeOfDayMessage();
            credentials = _credentialService.GetAllCredentialsWhereUserHasAuth(CurrentUser).ToList();
            CredentialCount = credentials.Count();
            VaultCount = _vaultService.GetVaultCountUserAuth(CurrentUser.OrganisationId);
            WeakPasswordCount = _credentialService.GetCountOfWeakPasswords(CurrentUser);
        }

        public IActionResult OnPostCreateCredential()
        {
            return RedirectToPage("/App/Create-credential");
        }

        public IActionResult OnPostGeneratePassword()
        {
            return RedirectToPage("/App/Generate-password");
        }

        public IActionResult OnPostLogout()
        {
            _userService.Logout(Response);
            return RedirectToPage("/Account/Login");
        }


    }
}
