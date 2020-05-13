using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Interfaces
{
    public interface IUserRepository: IGenericRepository
    {
         Task<IEnumerable<User>> GetAll();
         Task<User> GetUser(int id);

        
         void DeleteUser(int id);
         

         
    }
}