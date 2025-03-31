using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePathBackend.Objects;
using Microsoft.EntityFrameworkCore;

namespace BluePath_Backend.Repositories
{
    public class PatientInfoRepository : IPatientInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PatientInfo> CreateAsync(PatientInfo info)
        {
            _context.PatientInfos.Add(info);
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<PatientInfo> GetByUserIdAsync(string userId)
        {
            return await _context.PatientInfos
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<PatientInfo>> GetAllAsync()
        {
            return await _context.PatientInfos.ToListAsync();
        }
    }
}
