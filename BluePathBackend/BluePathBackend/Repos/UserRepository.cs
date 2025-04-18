﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BluePath_Backend.Objects;
using BluePath_Backend.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BluePathBackend.Interfaces;
using BluePathBackend.Objects;

namespace BluePathBackend.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}
