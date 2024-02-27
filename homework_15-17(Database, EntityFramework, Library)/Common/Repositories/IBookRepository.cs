using AccessToDB;
namespace Common.Repositories
{
    public interface IBookRepository
    {   
        Task<List<Book>> GetBooks();

        Task<Book> AddBook(Book book);

        Task<Book> UpdateBook(Book book);

        ValueTask<Book?> GetBook(int id);
    }
}