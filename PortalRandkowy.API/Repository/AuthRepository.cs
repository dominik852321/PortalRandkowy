using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        #region method public
        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dataContext.Users.Include(p =>p.Photos).FirstOrDefaultAsync(s=>s.UserName == username);

            if(user == null)
                  return null;

           if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null;  

            return user;
        }

        
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _dataContext.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
        }
        #endregion

        #region  method private
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
          
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var verify = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < verify.Length; i++)
                {
                    if(verify[i] != passwordHash[i])
                       return false;   
                }
                return true;
            }
        }

        #endregion
    }
}