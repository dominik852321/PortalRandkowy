using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Controllers
{
    [Authorize]
    [Route("api/users/{Userid}/photos")]
    [ApiController]
    public class PhotosController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IUserRepository userRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _userRepository = userRepository;
            _mapper = mapper;
            

            Account account = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos(int userId)
        {
            var photos = await _userRepository.GetPhotos(userId);
            var result = _mapper.Map<IEnumerable<PhotoForReturnDTO>>(photos);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoToUser(int Userid, [FromForm] PhotoForCreationDTO photoForCreation)
        {
            if(Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var userFromRepo = await _userRepository.GetUser(Userid);

            var file = photoForCreation.File;
            var uploadResult = new ImageUploadResult();

            if(file.Length>0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                      File = new FileDescription(file.Name, stream),
                      Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    
                    uploadResult=_cloudinary.Upload(uploadParams);
                }
            }

            photoForCreation.Url = uploadResult.Uri.ToString();
            photoForCreation.public_id = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreation);

            if(!userFromRepo.Photos.Any(p => p.MainPhoto))
                photo.MainPhoto = true;

            userFromRepo.Photos.Add(photo);

            if (await _userRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photo);
                return CreatedAtRoute("GetPhoto", new Photo { id = photoToReturn.id}, photoToReturn);
            }

            return BadRequest("Nie można dodać zdjęcia");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _userRepository.GetPhoto(id);

            var photoForReturn = _mapper.Map<PhotoForReturnDTO>(photoFromRepo);

            return Ok(photoForReturn);
        }

        [HttpPost("{id}/SetMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var user = await _userRepository.GetUser(userId);

            if(!user.Photos.Any(p => p.id == id))     
                 return Unauthorized();

            var photoFromRepo = await _userRepository.GetPhoto(id);

            if(photoFromRepo.MainPhoto)
                 return BadRequest("To jest główne zdjęcię");

            var currentMainPhoto =  await _userRepository.GetMainPhotoForUser(userId);
            currentMainPhoto.MainPhoto=false;
            photoFromRepo.MainPhoto=true;
            
            if(await _userRepository.SaveAll())
            {
                return Ok();     
            }

            return BadRequest("Nie można ustawić zdjęcia jako głównego");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _userRepository.GetUser(userId);

            if (!user.Photos.Any(p => p.id == id))
                return Unauthorized();

            var photoFromRepo = await _userRepository.GetPhoto(id);

            if (photoFromRepo.MainPhoto)
                return BadRequest("Nie można usunąć zdjęcia głównego");

            if (photoFromRepo.public_id != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.public_id);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                    _userRepository.Delete(photoFromRepo);
            }

            if (photoFromRepo.public_id == null)
                _userRepository.Delete(photoFromRepo);

            if (await _userRepository.SaveAll())
                return Ok();

            return BadRequest("Nie udało się usunąć zdjęcia");
        }
    }
}