using System;
using System.Threading.Tasks;
using API.data;
using API.dtos;
using API.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        public AccountController(DataContext dbContext) : base(dbContext) { }
        
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AppUser>> RegisterUser(RegisterDto registerDto)
        {
            Console.WriteLine(@"register user {0} {1}", registerDto.Name, registerDto.Password);

            AppUser existingUser = await DataContext.Users
                .SingleOrDefaultAsync(x => x.Name == registerDto.Name);

            if (existingUser != null)
            {
                Console.WriteLine("User Already Exists");
                return BadRequest("User already exists");
            }
            
            AppUser newUser = new AppUser {
                Name = registerDto.Name,
                Password = registerDto.Password,
                RegistrationDate = registerDto.RegistrationDate
            };

            await DataContext.Users.AddAsync(newUser);
            await DataContext.SaveChangesAsync();
            
            return newUser;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await DataContext.Users.FirstOrDefaultAsync(x =>
                x.Name.ToLower().Equals(loginDto.Name.ToLower()) &&
                x.Password.Equals(loginDto.Password));

            if (user == null)
            {
                return Unauthorized("Invalid user");
            }

            return user;
        }
    }
}