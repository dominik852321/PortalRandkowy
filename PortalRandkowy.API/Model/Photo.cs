using System;

namespace PortalRandkowy.API.Model
{
    public class Photo
    {
        public int id {get; set;}
        public string Url { get; set; }
        public string Description { get; set; }   
        public DateTime DateAdded {get; set;}    
        public Boolean MainPhoto {get; set;}     // Czy to główne zdjęcie?

        public User user {get; set;}             //Właściciel zdjęcia
        public int UserId {get; set;}            
    } 
}