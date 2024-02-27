using AccessToDB;
using Common.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Reader> DeleteReader(Reader reader)
        {
            _context.Readers.Remove(reader);
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

        public async Task<Reader> UpdateReader(Reader reader)
        {
            _context.Readers.Attach(reader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reader;
        }
    }
}
