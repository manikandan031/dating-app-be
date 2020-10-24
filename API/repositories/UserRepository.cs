using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.entities;
using Microsoft.EntityFrameworkCore;

namespace API.repositories
{
    public class UserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<AppUser> GetUser(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<AppUser> FindUserByName(string name)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public async void CreateUser(AppUser user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            Console.WriteLine(user);
        }
    }
}