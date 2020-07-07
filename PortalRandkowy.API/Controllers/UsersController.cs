using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
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

  

        public async Task<IActionResult> GetAll([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _userRepository.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if(string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "Mężczyzna" ? "Kobieta" : "Mężczyzna";
            }

            var users = await _userRepository.GetAll(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

      
        [HttpGet("{id}", Name = "GetUser")]
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

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var like = await _userRepository.GetLike(id, recipientId);

            if(like != null)
               return BadRequest("Już lubisz tego użytkownika");

            if(await _userRepository.GetUser(recipientId) == null)
               return NotFound();

            like = new Like
            {
                UserLikesId = id,
                UserIsLikedId = recipientId
            };

            _userRepository.Add<Like>(like);
            
            if(await _userRepository.SaveAll())
               return Ok();


            return BadRequest("Nie można polubić użytkownika");   
        }

        

       






     
         

        
    }
}