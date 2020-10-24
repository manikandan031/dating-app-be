using System;
using System.Threading.Tasks;
using API.dtos;
using API.entities;
using API.helpers;
using API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly JwtTokenService _tokenService;
        private readonly UserRepository _userRepository;

        public AccountController(JwtTokenService tokenService, UserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {
            Console.WriteLine(@"register user {0} {1}", registerDto.Name, registerDto.Password);

            var existingUser = await _userRepository.FindUserByName(registerDto.Name);

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

            _userRepository.CreateUser(newUser);
                
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
            var user = await _userRepository.FindUserByName(loginDto.Name);

            if (user == null || !user.Password.Equals(loginDto.Password))
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