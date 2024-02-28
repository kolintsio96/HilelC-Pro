using AccessToDB;
namespace Common.Services
{
    public interface ITokenService
    {
        string GetToken(IUser user);
    }
}
