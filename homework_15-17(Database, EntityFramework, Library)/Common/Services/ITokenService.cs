using AccessToDB;
namespace Common.Services
{
    public interface ITokenService
    {
        string GetToken(Reader reader);
    }
}
