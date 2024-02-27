using Common.Services;
using Common.Repositories;
using AccessToDB;

namespace Services
{
    internal class AccountService : IAccountService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ILibrarianRepository _librarianRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IReaderRepository readerRepository, ILibrarianRepository librarianRepository, ITokenService tokenService)
        {
            this._readerRepository = readerRepository;
            this._librarianRepository = librarianRepository;
            this._tokenService = tokenService;
        }

        public async Task<(IUser? account, string token)> Login(string login, string password, bool isReader = false)
        {
            IUser? account = null;
            if (isReader)
            {
                account = await _readerRepository.GetReaderByEmail(login);
            } else
            {
                account = await _librarianRepository.GetLibrarianByEmail(login);
            }
            
            if (account == null) return (null, null!);
            if (account.Password != password)
                throw new UnauthorizedAccessException();

            var token = _tokenService.GetToken(account);
            return (account, token);
        }
    }
}
