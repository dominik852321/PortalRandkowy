using System;

namespace PortalRandkowy.API.Dtos
{
    public class PhotosForDetailedDTO
    {
         public int id {get; set;}
        public string Url { get; set; }
        public string Description { get; set; }   
        public DateTime DateAdded {get; set;}    
        public Boolean MainPhoto {get; set;}     
    }
}