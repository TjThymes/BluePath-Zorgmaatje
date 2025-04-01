using BluePath_Backend.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePath_Backend.Interfaces
{
    public interface IDiaryRepository
    {
        Task<DiaryEntry> CreateAsync(DiaryEntry entry);
        Task<IEnumerable<DiaryEntry>> GetAllForUserAsync(string userId);
        Task<DiaryEntry> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<DiaryEntry> UpdateAsync(Guid id, DiaryEntry entry);
    }
}
