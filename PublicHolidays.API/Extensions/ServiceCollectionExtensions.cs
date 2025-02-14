using PublicHolidays.Services.ApiClients;

namespace PublicHolidays.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            _ = services.AddScoped<IEnricoApiClient, EnricoApiClient>()
                .AddHttpClient<IEnricoApiClient, EnricoApiClient>();

            return services;
        }
    }
}
