using Shared.Domain.Enums;

namespace PublicHolidays.Services.Abstractions
{
    public interface IHolidayService
    {
        Task<IEnumerable<Shared.Domain.Models.HolidayDomain>> GetHolidaysByYearAsync(int year, string countryCode);
        Task<DateStatusEnum> GetDateStatusAsync(DateOnly date, string countryCode);
        Task<int> GetMaxNumberOfFreeDaysInRowAsync(int year, string countryCode);
    }
}