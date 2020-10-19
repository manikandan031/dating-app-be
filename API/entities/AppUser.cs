using System;

namespace API.entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
        
        public DateTime RegistrationDate { get; set; }
    }
}