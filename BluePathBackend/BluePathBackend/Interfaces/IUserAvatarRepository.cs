using BluePath_Backend.Objects;
using System.Threading.Tasks;

namespace BluePath_Backend.Interfaces
{
    public interface IUserAvatarRepository
    {
        Task<UserAvatar> GetByUserIdAsync(string userId);
        Task<UserAvatar> SetOrUpdateAsync(string userId, UserAvatar avatarData);
    }
}
