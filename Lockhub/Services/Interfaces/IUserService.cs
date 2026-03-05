using Lockhub.Models;

namespace Lockhub.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(User user, HttpResponse response);
        void Logout(HttpResponse response);
        void Register(User user);
    }
}
