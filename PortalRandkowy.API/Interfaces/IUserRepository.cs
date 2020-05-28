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
         Task<IEnumerable<Photo>> GetPhotos(int userId);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);

        
         void DeleteUser(int id);
        
    }
}