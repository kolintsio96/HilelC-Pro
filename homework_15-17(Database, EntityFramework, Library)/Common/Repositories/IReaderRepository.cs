using AccessToDB;
namespace Common.Repositories
{
    public interface IReaderRepository
    {
        Task<Reader?> GetReaderByEmail(string email);
        
        Task<Reader> CreateReader(Reader reader);

        ValueTask<Reader?> GetReader(int id);

        Task<List<Reader>> GetReaders();
    }
}