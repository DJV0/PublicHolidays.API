using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Domain.Entities;
using Shared.Domain.Models;

namespace PublicHolidays.Domain.Repositories
{
    public class CountryRepository : GenericRepository<Country, CountryDomain>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(ApplicationDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ExistsAsync(string countryCode)
        {
            return await _context.Countries.AnyAsync(x => x.Code == countryCode);
        }

        public async Task<CountryDomain?> GetByCodeAsync(string countryCode)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Code == countryCode);
            return _mapper.Map<CountryDomain>(country);
        }
    }
}
