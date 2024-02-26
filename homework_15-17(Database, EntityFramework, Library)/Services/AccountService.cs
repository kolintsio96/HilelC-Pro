using Common.Services;
using Common.Repositories;
using AccessToDB;

namespace Services
{
    internal class AccountService : IAccountService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IReaderRepository userRepository, ITokenService tokenService)
        {
            this._readerRepository = userRepository;
            this._tokenService = tokenService;
        }
        public async Task<(Reader? reader, string token)> Login(string login, string password)
        {
            var reader = await _readerRepository.GetReaderByEmail(login);
            if (reader == null) return (null, null!);
            if (reader.Password != password)
                throw new UnauthorizedAccessException();

            var token = _tokenService.GetToken(reader);
            return (reader, token);
        }

        public async Task<(Reader reader, string token)> Register(Reader reader)
        {
            reader = await _readerRepository.CreateReader(reader);
            var token = _tokenService.GetToken(reader);

            return (reader, token);
        }
    }
}
