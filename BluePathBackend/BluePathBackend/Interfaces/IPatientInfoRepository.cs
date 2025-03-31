using BluePath_Backend.Objects;
using BluePathBackend.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePath_Backend.Interfaces
{
    public interface IPatientInfoRepository
    {
        Task<PatientInfo> CreateAsync(PatientInfo info);
        Task<PatientInfo> GetByUserIdAsync(string userId);
        Task<IEnumerable<PatientInfo>> GetAllAsync();
    }
}
