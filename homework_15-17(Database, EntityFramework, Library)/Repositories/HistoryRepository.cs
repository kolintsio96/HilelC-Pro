using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class HistoryRepository : IHistoryRepository
    {
        private readonly LibraryContext _context;

        public HistoryRepository(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<History> AddHistory(History history)
        {
            await _context.Histories.AddAsync(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public Task<List<History>> GetHistories()
        {
            return _context.Histories.AsNoTracking().ToListAsync();
        }

        public Task<List<History>> GetHistoryByReader(int? readerId)
        {
            return _context.Histories.Where(h => h.ReaderId == readerId).AsNoTracking().ToListAsync();
        }
    }
}
