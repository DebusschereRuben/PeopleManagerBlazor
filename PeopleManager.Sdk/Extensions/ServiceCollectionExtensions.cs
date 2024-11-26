using Microsoft.Extensions.DependencyInjection;
using PeopleManager.Sdk.Handlers;

namespace PeopleManager.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddScoped<AuthorizationHandler>();

            services.AddHttpClient("PeopleManagerApi", options =>
            {
                options.BaseAddress = new Uri(apiUrl);
            }).AddHttpMessageHandler<AuthorizationHandler>();
            
            services.AddScoped<IdentitySdk>();
            services.AddScoped<OrganizationSdk>();
            services.AddScoped<PersonSdk>();

            return services;
        }
    }
}
