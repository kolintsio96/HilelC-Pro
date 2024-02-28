using Common.Repositories;
using AccessToDB;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class DocumentRepository : IDocumentRepository
    {
        private readonly LibraryContext _context;

        public DocumentRepository(LibraryContext context)
        {
            this._context = context;
        }

        public Task<Document?> GetDocumentByType(string type)
        {
            return _context.Documents.FirstOrDefaultAsync(l => l.Type == type);
        }
        
        public Task<List<Document>> GetDocuments()
        {
            return _context.Documents.AsNoTracking().ToListAsync();
        }
    }
}
