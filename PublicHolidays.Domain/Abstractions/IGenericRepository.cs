namespace PublicHolidays.Domain.Abstractions
{
    public interface IGenericRepository<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<TDomain?> GetByIdAsync(int id);
        Task AddAsync(TDomain domainModel);
        Task AddRangeAsync(IEnumerable<TDomain> domainModels);
    }
}
