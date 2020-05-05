namespace PortalRandkowy.API.Model
{
    public class User
    {
        public int Userid {get; set;}
        public string UserName {get; set;}
        public byte[] Password {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}