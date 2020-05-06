using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {

        public readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        public async Task<IEnumerable<User>> GetAll()
            =>await _userRepository.GetAll();


        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
            =>await _userRepository.GetUser(id);


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit (int id,[FromBody] UserForEditDTO userToEdit)
        {
            var user = _userRepository.GetUser(id);
            if(ModelState.IsValid && user.Result!=null)
            {
                 await _userRepository.EditUser(userToEdit);
                 return Ok(userToEdit);
            }
            return NotFound("Użytkownik nie istnieje");
        }    

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUser(id);
            if(user.Result!=null)
               {
                    _userRepository.DeleteUser(id);
                   return Ok("Użytkownik został usunięty");
               }
            return NotFound("Użytkownik nie istnieje");   
        }

         

        
    }
}