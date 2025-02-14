using Shared.Domain.Models;

namespace PublicHolidays.Services.ApiClients
{
    public interface IEnricoApiClient
    {
        Task<IEnumerable<CountryDomain>> GetSupportedCountriesAsync();
        Task<CountryDomain?> GetCountryByCodeAsync(string countryCode);
        Task<List<HolidayDomain>> GetHolidaysForDateRangeAsync(DateOnly fromDate, DateOnly toDate, string countryCode);
    }
}