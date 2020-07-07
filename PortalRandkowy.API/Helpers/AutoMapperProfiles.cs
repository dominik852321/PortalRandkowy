using System.Linq;
using AutoMapper;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
            .ForMember(dest => dest.PhotoUrl,opt => { 
                opt.MapFrom(src => src.Photos.FirstOrDefault(s=>s.MainPhoto).Url);
            })
            .ForMember(dest => dest.Age, opt => {  
                opt.MapFrom(b => b.DateOfBirth.CalculateAge());
            });

            
            CreateMap<User, UserForDetailedDTO>().ForMember(s =>s.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p=>p.MainPhoto).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(s => s.DateOfBirth.CalculateAge());
            });

            CreateMap<Photo, PhotosForDetailedDTO>();
            CreateMap<UserForEditDTO, User>();
            CreateMap<PhotoForCreationDTO, Photo>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<UserForRegisterDTO, User>();
            CreateMap<MessageForCreationDTO, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDTO>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.MainPhoto).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.MainPhoto).Url));
           
              
        }
    }
}