using AccessToDB;
namespace Common.Services
{
    public interface IReaderService
    {
        Task<(Reader reader, string token)> Register(Reader reader);

        Task<Reader?> GetReader(int id);

        Task<Reader?> GetReaderByEmail(string email);
    }
}
