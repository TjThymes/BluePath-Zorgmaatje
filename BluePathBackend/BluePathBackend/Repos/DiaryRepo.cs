using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.EntityFrameworkCore;

namespace BluePath_Backend.Repositories
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly ApplicationDbContext _context;

        public DiaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DiaryEntry> CreateAsync(DiaryEntry entry)
        {
            _context.DiaryEntries.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<IEnumerable<DiaryEntry>> GetAllForUserAsync(string userId)
        {
            return await _context.DiaryEntries
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<DiaryEntry> GetByIdAsync(Guid id)
        {
            return await _context.DiaryEntries.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            var entry = await _context.DiaryEntries
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (entry == null)
                return false;

            _context.DiaryEntries.Remove(entry);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<DiaryEntry> UpdateAsync(Guid id, DiaryEntry entry)
        {
            var entryToUpdate = await _context.DiaryEntries.FindAsync(id);
            if (entryToUpdate == null)
                return null;
            entryToUpdate.Title = entry.Title;
            entryToUpdate.Note = entry.Note;
            entryToUpdate.CreatedAt= DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return entryToUpdate;
        }
    }
}
