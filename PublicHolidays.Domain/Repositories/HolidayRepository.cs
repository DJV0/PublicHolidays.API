using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Domain.Entities;
using Shared.Domain.Models;

namespace PublicHolidays.Domain.Repositories
{
    public class HolidayRepository : GenericRepository<Holiday, HolidayDomain>, IHolidayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HolidayRepository(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HolidayDomain>> GetHolidaysByYearAsync(int year, string countryCode)
        {
            var holidays = await _context.Holidays
                .Include(x => x.Country)
                .Where(x => x.Country.Code == countryCode && x.Date.Year == year)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HolidayDomain>>(holidays);
        }

        public async Task<IEnumerable<HolidayDomain>> GetHolidayByDateAsync(DateOnly date, string countryCode)
        {
            var holidays = await _context.Holidays
                .Include(x => x.Country)
                .Where(x => x.Country.Code == countryCode && x.Date == date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HolidayDomain>>(holidays);
        }
    }
}
