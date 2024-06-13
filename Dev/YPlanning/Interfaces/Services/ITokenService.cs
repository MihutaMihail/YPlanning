using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface ITokenService
    {
        string GetTokenValueByUserId(int? userId);
        bool CreateTokenForUser(User user);
        bool DeleteTokenByUserId(int? userId);
        bool DoesTokenExist(string? value);
    }
}
