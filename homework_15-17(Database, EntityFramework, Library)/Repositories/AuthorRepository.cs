using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<List<Author>> GetAuthorsByName(string? searchStr = null)
        {
            if (searchStr == null)
            {
               return await _context.Authors.AsNoTracking().ToListAsync();
            } else
            {
                return await _context.Authors.Where(a => a.Name.StartsWith(searchStr) || (a.Surname != null && a.Surname.StartsWith(searchStr))).AsNoTracking().ToListAsync();
            }
        }
    }
}
