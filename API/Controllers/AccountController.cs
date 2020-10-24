using System;
using System.Threading.Tasks;
using API.data;
using API.dtos;
using API.entities;
using API.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly JwtTokenService _tokenService;

        public AccountController(DataContext dbContext, JwtTokenService tokenService) : base(dbContext)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
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
            
            return new UserDto
            {
                Name = newUser.Name,
                Token = _tokenService.CreateToken(newUser)
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await DataContext.Users.FirstOrDefaultAsync(x =>
                x.Name.ToLower().Equals(loginDto.Name.ToLower()) &&
                x.Password.Equals(loginDto.Password));

            if (user == null)
            {
                return Unauthorized("Invalid user");
            }

            return new UserDto
            {
                Name= user.Name,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}