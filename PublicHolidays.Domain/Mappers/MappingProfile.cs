using AutoMapper;
using PublicHolidays.Domain.Entities;
using Shared.Domain.Models;

namespace PublicHolidays.Domain.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDomain>().ReverseMap();
            CreateMap<Holiday, HolidayDomain>().ReverseMap();
        }
    }
}
