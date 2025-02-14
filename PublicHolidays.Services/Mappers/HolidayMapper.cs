using PublicHolidays.Services.ApiClients.Contracts;
using Shared.Contracts.Holidays;
using Shared.Domain.Models;

namespace PublicHolidays.Services.Mappers
{
    public static class HolidayMapper
    {
        public static HolidayDomain ToDomain(this HolidayDto dto) =>
            new()
            {
                Date = new DateOnly(dto.Date.Year, dto.Date.Month, dto.Date.Day),
                Name = GetHolidayName(dto),
            };

        private static string GetHolidayName(HolidayDto dto)
        {
            var language = "en";

            var name = dto.Name.FirstOrDefault(x => x.Language == language);

            if (name == null)
            {
                return dto.Name.FirstOrDefault()?.Text ?? string.Empty;
            }

            return name.Text;
        }

        public static HolidayContract ToContract(this HolidayDomain domain) =>
            new()
            {
                Date = domain.Date,
                Name = domain.Name,
            };
    }
}
