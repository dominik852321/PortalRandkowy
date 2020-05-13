using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Repository
{
    public class UserRespository: GenericRepository, IUserRepository
    {
        public readonly DataContext _dataContext;
        public UserRespository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        
        public async Task<IEnumerable<User>> GetAll()
            =>await _dataContext.Users.Include(s=>s.Photos).ToListAsync();

        public async Task<User> GetUser(int id)
            =>await _dataContext.Users.Include(s=>s.Photos).FirstOrDefaultAsync(s=>s.Userid==id);
       
        
        public void DeleteUser(int id)
        {
               var user = _dataContext.Users.FirstOrDefault(s=>s.Userid==id);
               _dataContext.Users.Remove(user);
               _dataContext.SaveChanges();
        }

        
     
    }

}
