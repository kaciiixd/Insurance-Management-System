using IMS.Models.Domain;

namespace IMS.Repositories
{
    public interface IInsuranceRepository
    {
        Task<IEnumerable<Insurance>> GetAllAsync();
        Task<Insurance> GetAsync(Guid id);
        Task<Insurance> AddAsync(Insurance insurance);
        Task<Insurance?> UpdateAsync(Insurance insurance);
        Task<Insurance?> DeleteAsync(Guid id);
    }
}
