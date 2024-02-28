using Common.Services;
using Common.Repositories;
using AccessToDB;

namespace Services
{
    internal class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ITokenService _tokenService;

        public ReaderService(IReaderRepository readerRepository, ITokenService tokenService)
        {
            this._readerRepository = readerRepository;
            this._tokenService = tokenService;
        }

        public async Task<Reader?> GetReader(int id)
        {
            var reader = await _readerRepository.GetReader(id);
            return reader;
        }

        public async Task<Reader?> GetReaderByEmail(string email)
        {
            var reader = await _readerRepository.GetReaderByEmail(email);
            return reader;
        }

        public async Task<(Reader reader, string token)> Register(Reader reader)
        {
            reader = await _readerRepository.CreateReader(reader);
            var token = _tokenService.GetToken(reader);

            return (reader, token);
        }
    }
}
