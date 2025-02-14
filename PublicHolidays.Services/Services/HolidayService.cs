using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Domain.Entities;
using PublicHolidays.Services.Abstractions;
using Shared.Domain.Enums;
using Shared.Domain.Models;

namespace PublicHolidays.Services.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IDataHelper _dataHelper;

        public HolidayService(IHolidayRepository holidayRepository,
            IDataHelper dataHelper)
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

        public async Task<int> GetMaxNumberOfFreeDaysInRowAsync(int year, string countryCode)
        {
            await _dataHelper.FetchCountryHolidaysAsync(countryCode);

            var holidays = await _holidayRepository.GetHolidaysByYearAsync(year, countryCode);
            var freeDays = GetFreeDays(
                holidays.Select(x => x.Date).ToHashSet(), 
                year);

            return GetMaxFreedaysInRow(freeDays);

        }

        private int GetMaxFreedaysInRow(HashSet<DateOnly> freeDays)
        {
            int maxStreak = 0, currentStreak = 0;
            DateOnly? previousDay = null;

            foreach (var date in freeDays.OrderBy(d => d))
            {
                if (previousDay.HasValue && date == previousDay.Value.AddDays(1))
                {
                    currentStreak++;
                }
                else
                {
                    maxStreak = Math.Max(maxStreak, currentStreak);
                    currentStreak = 1;
                }
                previousDay = date;
            }

            return Math.Max(maxStreak, currentStreak);
        }

        private HashSet<DateOnly> GetFreeDays(HashSet<DateOnly> holidays, int year)
        {
            var freeDays = new HashSet<DateOnly>();
            var startDate = new DateOnly(year, 1, 1);
            var endDate = new DateOnly(year, 12, 31);

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(date))
                {
                    freeDays.Add(date);
                }
            }

            return freeDays;
        }
    }
}
