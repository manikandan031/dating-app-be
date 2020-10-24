using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.entities;
using API.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppUserController : ApiBaseController
    {
        private readonly UserRepository _userRepository;

        public AppUserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            Console.WriteLine("Inside Get All Users");
            return new ActionResult<IEnumerable<AppUser>>(await _userRepository.GetAllUsers());
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            Console.WriteLine("Inside Get User by id - {0}", id);
            return await _userRepository.GetUser(id);
        }
    }
}