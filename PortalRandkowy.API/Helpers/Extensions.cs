using System;

namespace PortalRandkowy.API.Helpers
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime DateBirth)
        {
            var age = DateTime.Today.Year - DateBirth.Year;
            
            if(DateBirth.DayOfYear > DateTime.Today.DayOfYear)
               age--;

            return age;
        }
    }
}