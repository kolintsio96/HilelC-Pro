using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class LibrarianRepository : ILibrarianRepository
    {
        private readonly LibraryContext _context;

        public LibrarianRepository(LibraryContext context)
        {
            this._context = context;
        }

        public Task<Librarian?> GetLibrarianByLogin(string login)
        {
            return _context.Librarians.FirstOrDefaultAsync(l => l.Login == login);
        }

        public Task<List<Librarian>> GetLibrarians()
        {
            return _context.Librarians.ToListAsync();
        }
    }
}
