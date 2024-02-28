using AccessToDB;
namespace Common.Repositories
{
    public interface IHistoryRepository
    {   
        Task<List<History>> GetHistories();

        Task<List<History>> GetHistoryByReader(int? readerId);

        Task<History> AddHistory(History history);
    }
}