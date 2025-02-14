using Shared.Domain.Models;

namespace PublicHolidays.Services.Abstractions
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDomain>> GetAllCountriesAsync();
    }
}
