using System.Threading.Tasks;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Interfaces
{
    public interface IAuthRepository
    {
         Task<User> Login(string username, string password);
         Task<User> Register(User user);
         Task<bool> UserExists(string username);
    }
}