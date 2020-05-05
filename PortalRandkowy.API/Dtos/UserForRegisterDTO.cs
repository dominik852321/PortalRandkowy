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

        
    }
}