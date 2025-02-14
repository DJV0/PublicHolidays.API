using Microsoft.Extensions.DependencyInjection;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.Services;

namespace PublicHolidays.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<ICountryService, CountryService>()
                .AddScoped<IHolidayService, HolidayService>()
                .AddScoped<DataHelper>();

            return services;
        }
    }
}
