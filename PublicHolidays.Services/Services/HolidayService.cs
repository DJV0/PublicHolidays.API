using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Services.Abstractions;
using Shared.Domain.Enums;
using Shared.Domain.Models;

namespace PublicHolidays.Services.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly DataHelper _dataHelper;

        public HolidayService(IHolidayRepository holidayRepository,
            DataHelper dataHelper)
        {
            _holidayRepository = holidayRepository;
            _dataHelper = dataHelper;
        }

        public async Task<IEnumerable<HolidayDomain>> GetHolidaysByYearAsync(int year, string countryCode)
        {
            await _dataHelper.FetchCountryHolidaysAsync(countryCode);

            return await _holidayRepository.GetHolidaysByYearAsync(year, countryCode);
        }

        public async Task<DateStatusEnum> GetDateStatusAsync(DateOnly date, string countryCode)
        {
            await _dataHelper.FetchCountryHolidaysAsync(countryCode);

            var holidaysByDate = await _holidayRepository.GetHolidayByDateAsync(date, countryCode);

            if (holidaysByDate.Any())
                return DateStatusEnum.Holiday;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return DateStatusEnum.Freeday;

            return DateStatusEnum.Workday;
        }
    }
}
