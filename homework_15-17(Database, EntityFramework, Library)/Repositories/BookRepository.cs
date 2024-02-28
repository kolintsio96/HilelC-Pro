using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<Book> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public ValueTask<Book?> GetBook(int id)
        {
            return _context.Books.FindAsync(id);
        }

        public Task<List<Book>> GetBooks()
        {
            return _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _context.Books.Attach(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
