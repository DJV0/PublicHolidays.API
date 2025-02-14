using Newtonsoft.Json;
using PublicHolidays.Services.ApiClients.Contracts;
using PublicHolidays.Services.Mappers;
using Shared.Domain.Models;

namespace PublicHolidays.Services.ApiClients
{
    public class EnricoApiClient : IEnricoApiClient
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrl = "https://kayaposoft.com/enrico/json/v3.0/";
        private const string dateFormat = "yyyy-MM-dd";

        public EnricoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CountryDomain>> GetSupportedCountriesAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl + "getSupportedCountries");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<CountryDto>>(result) ?? [];

            return countries.Select(x => x.ToDomain());
        }

        //TODO: create BaseApiClient class
        public async Task<CountryDomain?> GetCountryByCodeAsync(string countryCode)
        {
            var response = await _httpClient.GetAsync(_apiUrl + "getSupportedCountries");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<CountryDto>>(result) ?? [];

            return countries
                .Where(x => x.Code == countryCode)
                .Select(x => x.ToDomain())
                .FirstOrDefault();
        }

        public async Task<List<HolidayDomain>> GetHolidaysForDateRangeAsync(DateOnly fromDate, DateOnly toDate, string countryCode)
        {
            var response = await _httpClient.GetAsync(_apiUrl + 
                $"getHolidaysForDateRange?fromDate={fromDate.ToString(dateFormat)}&toDate={toDate.ToString(dateFormat)}" +
                $"&country={countryCode}&holidayType=all");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var holidays = JsonConvert.DeserializeObject<List<HolidayDto>>(result) ?? [];

            return holidays.Select(x => x.ToDomain()).ToList();
        }
    }
}
