namespace PublicHolidays.Services.Abstractions
{
    public interface IDataHelper
    {
        Task FetchCountryHolidaysAsync(string countryCode);
    }
}