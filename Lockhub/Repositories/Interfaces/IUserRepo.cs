using Lockhub.Models;
namespace Lockhub.Repositories.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        User GetUserById(int id);
        bool UserWithEmailAlreadyExist(string email);
        void AddUser(User user);
        void RemoveUser(string email);
        void UpdateUser(User user);
    }
}
