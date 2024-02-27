using AccessToDB;
namespace Common.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthorsByName(string? searchStr);
    }
}