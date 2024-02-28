using Common.Services;
using Common.Repositories;
using AccessToDB;

namespace Services
{
    internal class LibrarianService : ILibrarianService
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly ITokenService _tokenService;

        public LibrarianService(ILibrarianRepository librarianRepository, ITokenService tokenService)
        {
            this._librarianRepository = librarianRepository;
            this._tokenService = tokenService;
        }

        public async Task<Librarian?> GetLibrarianByEmail(string email)
        {
            var librarian = await _librarianRepository.GetLibrarianByEmail(email);
            return librarian;
        }
        
        public async Task<Librarian?> GetLibrarian(int id)
        {
            var librarian = await _librarianRepository.GetLibrarian(id);
            return librarian;
        }

        public async Task<(Librarian librarian, string token)> Register(Librarian librarian)
        {
            librarian = await _librarianRepository.CreateLibrarian(librarian);
            var token = _tokenService.GetToken(librarian);

            return (librarian, token);
        }

        public async Task<List<Librarian>> GetLibrarians()
        {
            var librarian = await _librarianRepository.GetLibrarians();
            return librarian;
        }
    }
}
