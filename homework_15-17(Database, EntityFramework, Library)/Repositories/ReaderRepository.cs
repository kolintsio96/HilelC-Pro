using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    internal class ReaderRepository : IReaderRepository
    {
        private readonly LibraryContext _context;

        public ReaderRepository(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<Reader> CreateReader(Reader reader)
        {
            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public ValueTask<Reader?> GetReader(int id)
        {
            return _context.Readers.FindAsync(id);
        }

        public Task<Reader?> GetReaderByEmail(string login)
        {
            return _context.Readers.FirstOrDefaultAsync(l => l.Email == login);
        }

        public Task<List<Reader>> GetReaders()
        {
            return _context.Readers.AsNoTracking().ToListAsync();
        }
    }
}
