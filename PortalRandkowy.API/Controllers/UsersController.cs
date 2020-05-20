using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<IActionResult> GetAll()
        {
          
            var users = await _userRepository.GetAll();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);
            return Ok(usersToReturn);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
          var user = await _userRepository.GetUser(id);
          var userToReturn = _mapper.Map<UserForDetailedDTO>(user);
          return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, UserForEditDTO userForEdit)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var userFromRepo = await _userRepository.GetUser(id);
            _mapper.Map(userForEdit, userFromRepo);

            if(await _userRepository.SaveAll())
                return NoContent();
            
                throw new Exception($"Aktualizacja użytkownika o id: {id} nie powiodła sie podczas zapisu w bazie"); 
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