using PublicHolidays.Domain.Entities;
using Shared.Domain.Models;

namespace PublicHolidays.Domain.Abstractions
{
    public interface IHolidayRepository : IGenericRepository<HolidayDomain>
    {
        Task<IEnumerable<HolidayDomain>> GetHolidaysByYearAsync(int year, string countryCode);
        Task<IEnumerable<HolidayDomain>> GetHolidayByDateAsync(DateOnly date, string countryCode);
    }
}
