using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Interfaces
{
    public interface IUserRepository
    {
         Task<IEnumerable<User>> GetAll();
         Task<User> GetUser(int id);

         Task<User> EditUser(UserForEditDTO userToEdit);
         void DeleteUser(int id);
         

         
    }
}