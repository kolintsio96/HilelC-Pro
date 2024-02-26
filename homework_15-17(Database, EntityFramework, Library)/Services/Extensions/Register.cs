using Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions
{
    public static class Register
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
