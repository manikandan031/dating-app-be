using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AppUserController : ApiBaseController
    {
        public AppUserController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            Console.WriteLine("Inside Get All Users");
            return await DataContext.Users.ToListAsync();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            Console.WriteLine("Inside Get User by id - {0}", id);
            return await DataContext.Users.FindAsync(id);
        }
    }
}