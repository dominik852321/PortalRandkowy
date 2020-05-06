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
    public class UserRespository: IUserRepository
    {
        public readonly DataContext _dataContext;
        public UserRespository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
        public async Task<IEnumerable<User>> GetAll()
            =>await _dataContext.Users.ToListAsync();

        public async Task<User> GetUser(int id)
            =>await _dataContext.Users.FirstOrDefaultAsync(s=>s.Userid==id);
       
        
        public void DeleteUser(int id)
        {
               var user = _dataContext.Users.FirstOrDefault(s=>s.Userid==id);
               _dataContext.Users.Remove(user);
               _dataContext.SaveChanges();
        }

        public async Task<User> EditUser(UserForEditDTO userToEdit)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(s=>s.Userid==userToEdit.id);
            user.UserName=userToEdit.Username;

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }

}
