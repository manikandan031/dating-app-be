using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class AppUserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AppUserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            Console.WriteLine("Inside Get All Users");
            return await _dataContext.Users.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            Console.WriteLine("Inside Get User by id - {0}", id);
            return await _dataContext.Users.FindAsync(id);
        }
    }
}