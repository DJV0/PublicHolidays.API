using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.ApiClients;

namespace PublicHolidays.Services
{
    public class DataHelper : IDataHelper
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEnricoApiClient _enricoApiClient;

        public DataHelper(ICountryRepository countryRepository, IHolidayRepository holidayRepository,
            IEnricoApiClient enricoApiClient)
        {
            _countryRepository = countryRepository;
            _holidayRepository = holidayRepository;
            _enricoApiClient = enricoApiClient;
        }

        public async Task FetchCountryHolidaysAsync(string countryCode)
        {
            var isCountryExist = await _countryRepository.ExistsAsync(countryCode);
            if (isCountryExist) return;

            var country = await _enricoApiClient.GetCountryByCodeAsync(countryCode);
            if (country == null)
                throw new ArgumentOutOfRangeException(countryCode);

            await _countryRepository.AddAsync(country);

            // The toDate value should be considered separately
            var toDate = DateOnly.FromDateTime(DateTime.Now.AddYears(20));
            var holidays = await _enricoApiClient.GetHolidaysForDateRangeAsync(country.FromDate, toDate, countryCode);
            var dbCountry = await _countryRepository.GetByCodeAsync(countryCode)
                ?? throw new ArgumentNullException(countryCode);

            foreach (var holiday in holidays)
            {
                holiday.CountryId = dbCountry.Id;
            }

            await _holidayRepository.AddRangeAsync(holidays);
        }
    }
}
