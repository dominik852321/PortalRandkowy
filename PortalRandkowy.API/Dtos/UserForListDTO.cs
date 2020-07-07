using System;
using System.Collections.Generic;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Dtos
{
    public class UserForListDTO
    {
        public int Userid {get; set;}
        public string UserName {get; set;}

        public string Gender { get; set; }       
        public int Age {get; set;}
      
        

        public string City {get; set;}           
        public string Country {get; set;}      


        public int Growth {get; set;}      
        public int Weight {get; set;}            
        public string MartialStatus {get; set;}  
        public string ColorSkin {get; set;}      
        public string Education {get; set;}      
        public string Profession {get; set;}   

        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
    
        public string PhotoUrl {get; set;} 
    }
}