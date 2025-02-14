using PublicHolidays.Services.ApiClients.Contracts;
using Shared.Contracts.Countries;
using Shared.Domain.Models;

namespace PublicHolidays.Services.Mappers
{
    public static class CountryMapper
    {
        public static CountryContract ToContract(this CountryDomain domain) =>
            new()
            {
                Code = domain.Code,
                Name = domain.Name,
            };

        public static CountryDomain ToDomain(this CountryDto dto) =>
            new()
            {
                Code = dto.Code,
                Name = dto.Name,
                FromDate = new DateOnly(dto.FromDate.Year, dto.FromDate.Month, dto.FromDate.Day),
                ToDate = new DateOnly(
                    dto.ToDate.Year > DateOnly.MaxValue.Year ? DateOnly.MaxValue.Year : dto.ToDate.Year,
                    dto.ToDate.Month, 
                    dto.ToDate.Day),
            };
    }
}
