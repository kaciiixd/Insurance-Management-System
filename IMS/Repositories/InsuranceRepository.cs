using IMS.Data;
using IMS.Models.Domain;
using IMS.Repositories;
using Microsoft.EntityFrameworkCore;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly IMSDbContext imsDbContext;

    public InsuranceRepository(IMSDbContext imsDbContext)
    {
        this.imsDbContext = imsDbContext;
    }

    public async Task<Insurance> AddAsync(Insurance insurance)
    {
        await imsDbContext.AddAsync(insurance);
        await imsDbContext.SaveChangesAsync();
        return insurance;
    }

    public async Task<Insurance?> DeleteAsync(Guid id)
    {
        var existingInsurance = await imsDbContext.Insurances.FindAsync(id);

        if (existingInsurance != null)
        {
            imsDbContext.Insurances.Remove(existingInsurance);
            await imsDbContext.SaveChangesAsync();
            return existingInsurance;
        }
        return null;
    }

    public async Task<IEnumerable<Insurance>> GetAllAsync()
    {
        return await imsDbContext.Insurances.Include(x => x.Client).ToListAsync();
    }

    public async Task<Insurance?> GetAsync(Guid id)
    {
        return await imsDbContext.Insurances.Include(i => i.Client).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Insurance?> UpdateAsync(Insurance insurance)
    {
        var existingInsurance = await imsDbContext.Insurances
            .FirstOrDefaultAsync(x => x.Id == insurance.Id);

        if (existingInsurance != null)
        {
            // Update only the fields that are modified
            if (existingInsurance.InsuranceType != insurance.InsuranceType)
                existingInsurance.InsuranceType = insurance.InsuranceType;

            if (existingInsurance.Sum != insurance.Sum)
                existingInsurance.Sum = insurance.Sum;

            if (existingInsurance.Subject != insurance.Subject)
                existingInsurance.Subject = insurance.Subject;

            if (existingInsurance.ValidFrom != insurance.ValidFrom)
                existingInsurance.ValidFrom = insurance.ValidFrom;

            if (existingInsurance.ValidUntil != insurance.ValidUntil)
                existingInsurance.ValidUntil = insurance.ValidUntil;

            await imsDbContext.SaveChangesAsync();

            return existingInsurance;
        }

        return null;
    }


}
