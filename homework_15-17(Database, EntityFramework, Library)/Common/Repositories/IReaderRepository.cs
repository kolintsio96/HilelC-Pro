using AccessToDB;
namespace Common.Repositories
{
    public interface IReaderRepository
    {
        Task<Reader?> GetReaderByEmail(string email);
        
        Task<Reader> CreateReader(Reader reader);

        Task<Reader> UpdateReader(Reader reader);

        Task<Reader> DeleteReader(Reader reader);

        ValueTask<Reader?> GetReader(int id);

        Task<List<Reader>> GetReaders();
    }
}