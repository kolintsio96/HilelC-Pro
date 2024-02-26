using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Extensions
{
    public static class Register
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<ILibrarianRepository, LibrarianRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            return services;
        }
    }
}
