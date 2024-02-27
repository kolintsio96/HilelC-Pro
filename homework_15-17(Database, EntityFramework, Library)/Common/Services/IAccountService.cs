using AccessToDB;
namespace Common.Services
{
    public interface IAccountService
    {
        Task<(IUser? account, string token)> Login(string login, string password, bool isReader = false);
    }
}
