using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services;
using System.Diagnostics;

namespace Lockhub.Repositories
{
    public class UserRepo : IUserRepo
    {
        private JsonFileService<User> _jsonService;
        private List<User> _users;
        public UserRepo(JsonFileService<User> jsonFileService)
        {
            _jsonService = jsonFileService;
            _users = _jsonService.GetJsonObjects().ToList();
            //_users = new List<User>();
            //_users.Add(new User(1, "Admin", "Admin", "admin@lockhub.com", "admin123", "12345678", 1, DateTime.Now, DateTime.Now));
            //_jsonService.SaveJsonObjects(_users);

        }
        public void AddUser(User user)
        {
            bool userExistBool = UserWithEmailAlreadyExist(user.Email);
            if (!userExistBool)
            {
                _users.Add(user);
                _jsonService.SaveJsonObjects(_users);
            } else
            {
                throw new Exception("User with that email already exists");
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public bool UserWithEmailAlreadyExist(string email)
        {
            foreach (User u in _users)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            foreach (User u in _users)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    return u;
                }
            }
            throw new Exception("User not found");
        }

        public User GetUserById(int id)
        {
            foreach (User u in _users)
            {
                if (u.UserId == id)
                {
                    return u;
                }
            }
            throw new Exception("User not found");
        }

        public void RemoveUser(string email)
        {
            foreach (User u in _users)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    _users.Remove(u);
                    break;
                }
            }
            _jsonService.SaveJsonObjects(_users);
        }

        public void UpdateUser(User user)
        {
            foreach (User u in _users)
            {
                if (u.UserId == user.UserId)
                {
                    u.Firstname = user.Firstname;
                    u.Lastname = user.Lastname;
                    u.Email = user.Email;
                    u.HashPassword = user.HashPassword;
                    u.Phone = user.Phone;
                    u.RoleId = user.RoleId;
                }
            }
            _jsonService.SaveJsonObjects(_users);
        }
    }
}
