using System;
using System.Collections.Generic;

namespace PortalRandkowy.API.Model
{
    public class User
    {
        public int Userid {get; set;}
        public string UserName {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        

        // Podstawowe informacje
        public string Gender { get; set; }       
        public DateTime DateOfBirth {get; set;}  
        public string ZodiacSign {get; set;}     
        public DateTime Created {get; set;}     
        public DateTime LastActive {get; set;}   
        public string City {get; set;}           
        public string Country {get; set;}        
        

        // Zakładka info
        public int Growth {get; set;}            
        public string ColorEye {get; set;}      
        public int Weight {get; set;}            
        public string MartialStatus {get; set;}  
        public string ColorSkin {get; set;}      
        public string Education {get; set;}      
        public string Profession {get; set;}   
        public string Langueches {get; set;}     
        public string Children {get; set;}       

        // Zakładka o mnie
        public string Motto {get; set;}
        public string Description {get; set;}
        public string Personality {get; set;}
        public string LookingFor {get; set;}

        // Zakładka Pasje, Zainteresowania
        public string Interest {get; set;}
        public string FreeTime {get; set;}
        public string Sport {get; set;}
        public string Movies {get; set;}
        public string Music {get; set;}

        // Zakładka Preferencje 
        public string ILike { get; set; }
        public string INotLike { get; set; }
        public string MakesMeLaugh {get; set;}
        public string ItFeelsBestIn {get; set;}
        public string FriendsWouldDescribeMe {get; set;}

        // Zakładka zdjęcia
        public ICollection<Photo> Photos { get; set; }    
        public ICollection<Like> UserLikes { get; set; }  // użytkownik lubi
        public ICollection<Like> UserIsLiked { get; set; } // użytkownik  jest lubiany

        public ICollection<Message> MessagesSend { get; set; } 
        public ICollection<Message> MessagesRecived { get; set; }



    }
}