using IMS.Models.Domain;

namespace IMS.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetAsync(Guid id, bool includeInsurances = false);
        Task<Client> AddAsync(Client client);
        Task<Client?> UpdateAsync(Client client);
        Task<Client?> DeleteAsync(Guid id);
    }
}
