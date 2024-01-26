using IMS.Data;
using IMS.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMSDbContext imsDbContext;

        public ClientRepository(IMSDbContext imsDbContext)
        {
            this.imsDbContext = imsDbContext;
        }

        public async Task<Client> AddAsync(Client client)
        {
            await imsDbContext.Clients.AddAsync(client);
            await imsDbContext.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> DeleteAsync(Guid id)
        {
            var existingClient = await imsDbContext.Clients.FindAsync(id);

            if (existingClient != null)
            {
                imsDbContext.Clients.Remove(existingClient);
                await imsDbContext.SaveChangesAsync();
                return existingClient;
            }
            return null;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await imsDbContext.Clients.ToListAsync();
        }

        public async Task<Client?> GetAsync(Guid id, bool includeInsurances = false)
        {
            var query = imsDbContext.Clients.AsQueryable();

            if (includeInsurances)
            {
                query = query.Include(c => c.Insurances);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Client?> UpdateAsync(Client client)
        {
            var existingClient = await imsDbContext.Clients.FindAsync(client.Id);

            if (existingClient != null)
            {
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Email = client.Email;
                existingClient.DateOfBirth = client.DateOfBirth;
                existingClient.Address = client.Address;
                existingClient.ContactNumber = client.ContactNumber;

                await imsDbContext.SaveChangesAsync();

                return existingClient;
            }
            return null;
        }
    }
}
