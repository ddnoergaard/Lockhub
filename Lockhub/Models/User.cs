using System.ComponentModel.DataAnnotations;

namespace Lockhub.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Min. 8 characters.")]
        public string HashPassword { get; set; }
        //[StringLength(100, MinimumLength = 8, ErrorMessage = "Min. 8 digits")]
        public string? Phone { get; set; }
        public int OrganisationId { get; set; } = 1;
        public int RoleId { get; set; } //FK to RoleId
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CurrentLogin { get; set; }

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
