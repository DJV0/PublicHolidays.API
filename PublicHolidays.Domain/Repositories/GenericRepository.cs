using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHolidays.Domain.Abstractions;

namespace PublicHolidays.Domain.Repositories
{
    public abstract class GenericRepository<TEntity, TDomain> : IGenericRepository<TDomain> 
        where TEntity : class
        where TDomain : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TDomain>>(entities);
        }

        public async Task<TDomain?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDomain>(entity);
        }

        public async Task AddAsync(TDomain domainModel)
        {
            var entity = _mapper.Map<TEntity>(domainModel);
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TDomain> domainModels)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(domainModels);
            _dbSet.AddRange(entities);

            await _context.SaveChangesAsync();
        }
    }
}
