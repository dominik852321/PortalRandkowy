using System;
using System.ComponentModel.DataAnnotations;

namespace PortalRandkowy.API.Dtos
{
    public class UserForRegisterDTO
    {
        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage="Hasło jest wymagane")]
        [StringLength(12, MinimumLength=6, ErrorMessage="Hasło musi się składać od 6 do 12 znaków")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set;}
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int Growth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDTO()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }

        
    }
}