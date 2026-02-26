namespace Lockhub.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; } //FK to RoleId
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {
            
        }

        public User(int userId, string firstname, string lastname, string email, string hashPassword, 
            string phone, int roleId, DateTime birthDate, DateTime createdAt)
        {
            UserId = userId;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            HashPassword = hashPassword;
            Phone = phone;
            RoleId = roleId;
            BirthDate = birthDate;
            CreatedAt = createdAt;
        }
    }
}
