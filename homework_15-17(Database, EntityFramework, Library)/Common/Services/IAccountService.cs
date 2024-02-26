using AccessToDB;
namespace Common.Services
{
    public interface IAccountService
    {
        Task<(Reader? reader, string token)> Login(string login, string password);

        Task<(Reader reader, string token)> Register(Reader reader);
    }
}
