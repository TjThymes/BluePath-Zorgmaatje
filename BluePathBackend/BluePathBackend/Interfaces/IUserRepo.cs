using BluePath_Backend.Objects;
using BluePathBackend.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePathBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<bool> CreateAsync(ApplicationUser user, string password);
    }
}

