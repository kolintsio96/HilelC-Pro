using AccessToDB;
namespace Common.Repositories
{
    public interface ILibrarianRepository
    {
        Task<Librarian> CreateLibrarian(Librarian librarian);

        Task<Librarian?> GetLibrarianByEmail(string email);

        Task<List<Librarian>> GetLibrarians();

        ValueTask<Librarian?> GetLibrarian(int id);
    }
}