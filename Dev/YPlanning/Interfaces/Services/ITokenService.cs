using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface ITokenService
    {
        string GetTokenValueByUserId(int? userId);
        Token GetTokenByValue(string? value);
        bool CreateTokenForUser(User user);
        bool DeleteTokenByUserId(int? userId);
        bool DoesTokenExist(string? value);
    }
}
