using _Flagsmith.Adapters;
using Flagsmith;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _Flagsmith.Configuration
{
    public static class FlagsmithConfigurations
    {
        public static IServiceCollection AddFlagsmith(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new FlagsmithConfiguration()
            {
                ApiUrl = configuration.GetSection("Flagsmith:ApiUrl").Value,
                EnvironmentKey = configuration.GetSection("Flagsmith:EnvironmentKey").Value,
            };

            var client = new FlagsmithClient(config);

            services.AddSingleton(client);
            services.AddSingleton<IFlagsmithAdapter, FlagsmithAdapter>();

            return services;
        }
    }
}
