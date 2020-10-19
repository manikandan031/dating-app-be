using System;

namespace API.dtos
{
    public class RegisterDto
    {
        public string Name { get; set; }

        public string Password { get; set; }
        
        public DateTime RegistrationDate { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + " , Password: " + Password + " , RegistrationDate: " + RegistrationDate;
        }
    }
}