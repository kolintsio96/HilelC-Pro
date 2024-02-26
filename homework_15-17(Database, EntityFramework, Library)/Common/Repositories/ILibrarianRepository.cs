using AccessToDB;
namespace Common.Repositories
{
    public interface ILibrarianRepository
    {
        Task<Librarian?> GetLibrarianByLogin(string login);

        Task<List<Librarian>> GetLibrarians();
    }
}