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

        public async Task<Librarian> CreateLibrarian(Librarian librarian)
        {
            await _context.Librarians.AddAsync(librarian);
            await _context.SaveChangesAsync();
            return librarian;
        }

        public ValueTask<Librarian?> GetLibrarian(int id)
        {
            return _context.Librarians.FindAsync(id);
        }

        public Task<Librarian?> GetLibrarianByEmail(string email)
        {
            return _context.Librarians.FirstOrDefaultAsync(l => l.Email == email);
        }

        public Task<List<Librarian>> GetLibrarians()
        {
            return _context.Librarians.AsNoTracking().ToListAsync();
        }
    }
}
