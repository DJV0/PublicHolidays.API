using PublicHolidays.Domain.Entities;
using Shared.Domain.Models;

namespace PublicHolidays.Domain.Abstractions
{
    public interface ICountryRepository : IGenericRepository<CountryDomain>
    {
        Task<CountryDomain?> GetByCodeAsync(string counrtyCode);

        Task<bool> ExistsAsync(string countryCode);
    }
}
