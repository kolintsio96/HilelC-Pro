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
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            return services;
        }
    }
}
