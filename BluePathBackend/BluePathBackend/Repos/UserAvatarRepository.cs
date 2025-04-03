using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BluePathBackend.Repos
{
    public class UserAvatarRepository : IUserAvatarRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAvatarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserAvatar> GetByUserIdAsync(string userId)
        {
            return await _context.Avatars.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<UserAvatar> SetOrUpdateAsync(string userId, UserAvatar avatarData)
        {
            var existing = await _context.Avatars.FirstOrDefaultAsync(a => a.UserId == userId);

            if (existing == null)
            {
                avatarData.UserId = userId;
                _context.Avatars.Add(avatarData);
            }
            else
            {
                existing.Head = avatarData.Head;
                existing.Body = avatarData.Body;
                existing.Legs = avatarData.Legs;
            }

            await _context.SaveChangesAsync();
            return existing ?? avatarData;
        }
    }
}
