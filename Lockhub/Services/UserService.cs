using Lockhub.Models;
using Lockhub.Repositories;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;

namespace Lockhub.Services
{
    public class UserService : IUserService
    {
        private JwtService _jwtService;
        private IUserRepo _userRepo;

        public UserService(JwtService jwtService, IUserRepo userRepo)
        {
            _jwtService = jwtService;
            _userRepo = userRepo;
        }

        public async Task<bool> LoginAsync(User user, HttpResponse response)
        {
            User? tempUser;
            if (_userRepo.UserWithEmailAlreadyExist(user.Email)) 
            {
                tempUser = await _userRepo.GetUserByEmail(user.Email);
                if (tempUser.Email == user.Email && tempUser.HashPassword == user.HashPassword)
                {
                    var token = _jwtService.GenerateToken(user.UserId, user.OrganisationId, user.RoleId);

                    response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(8)
                    });
                    _userRepo.UpdateUserLastLoginProp(tempUser.UserId);
                    return true;
                } else
                {
                    return false;
                }
            }
            return false;
        }

        public void Logout(HttpResponse response)
        {
            response.Cookies.Delete("jwt");
        }

        public void Register(User user)
        {
            user.UserId = GetSetNewId();
            user.CreatedAt = DateTime.Now;
            _userRepo.AddUser(user);
        }

        private int GetSetNewId()
        {
            List<User> tempList = _userRepo.GetAllUsers().ToList();
            int top = tempList.Count;
            int newId = tempList[top - 1].UserId;
            if (newId == 0)
            {
                newId = top;
            }
            int highestId = 0;
            foreach (User u in tempList)
            {
                if (u.UserId > highestId) highestId = u.UserId;
                if (u.UserId == newId)
                {
                    newId = ++highestId;
                }
            }
            return newId;
        }
    }
}
