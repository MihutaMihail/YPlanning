using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface ITokenRepository
    {
        Token GetTokenByUserId(int? userId);
        Token GetTokenByValue(string? value);
        bool CreateTokenForUser(Token createToken);
        bool DeleteToken(Token deleteToken);
        bool DoesTokenExist(string? value);
        bool Save();
    }
}
