using System;
using System.Collections;
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
     [Route("api/users/{userId}/[controller]")]
     [ApiController]
      public class MessagesController: ControllerBase
    {
        public readonly IUserRepository _repository;
        public readonly IMapper _mapper;
        public MessagesController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDTO messageForCreation)
        {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

             messageForCreation.SenderId = userId;
             var recipient = await _repository.GetUser(messageForCreation.RecipientId);

             if(recipient == null)
                return BadRequest("Nie można znalezc użytkownika");

             var message = _mapper.Map<Message>(messageForCreation);

             _repository.Add(message);
            
            if (await _repository.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturnDTO>(message);
                 return CreatedAtAction(nameof(GetMessage), new { Id = messageToReturn.Id}, messageToReturn);
                
            }

             throw new Exception("Utworzenie wiadomości nie powiodło się przy zapisie");  
        }

        [HttpGet("{id}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var messageFromRepo = await _repository.GetMessage(id);  

            if(messageFromRepo == null)
               return NotFound();

            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            messageParams.UserId = userId;
            var messagesFromRepo = await _repository.GetMessagesForUser(messageParams);
            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize, 
                                   messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messagesToReturn);           
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var messagesForRepo = await _repository.GetMessageThread(userId, recipientId);
            
            if(messagesForRepo==null)
               return BadRequest("Nie ma takiej wiadomości");

            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messagesForRepo);

            return Ok(messageThread);   
        }


       
    }
}