using AccessToDB;
namespace Common.Services
{
    public interface ILibrarianService
    {
        Task<(Librarian librarian, string token)> Register(Librarian librarian);

        Task<Librarian?> GetLibrarian(int id);

        Task<Librarian?> GetLibrarianByEmail(string email);

        Task<List<Librarian>> GetLibrarians();
    }
}
