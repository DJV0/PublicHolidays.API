using Microsoft.Extensions.DependencyInjection;
using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Domain.Repositories;

namespace PublicHolidays.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            _ = services.AddTransient<ICountryRepository, CountryRepository>()
                .AddTransient<IHolidayRepository, HolidayRepository>();

            return services;
        }
    }
}
