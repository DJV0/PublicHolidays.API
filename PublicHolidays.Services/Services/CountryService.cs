using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.ApiClients;
using Shared.Domain.Models;

namespace PublicHolidays.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IEnricoApiClient _enricoApiClient;

        public CountryService(ICountryRepository countryRepository, IEnricoApiClient enricoApiClient)
        {
            _countryRepository = countryRepository;
            _enricoApiClient = enricoApiClient;
        }

        public async Task<IEnumerable<CountryDomain>> GetAllCountriesAsync()
        {
            return await _enricoApiClient.GetSupportedCountriesAsync();
        }
    }
}
