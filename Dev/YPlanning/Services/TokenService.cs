using YPlanning.Helper;
using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public bool CreateTokenForUser(User user)
        {;
            var tokenValue = TokenHelper.GenerateSimpleToken(30);
            var encryptedTokenValue = TokenHelper.EncryptToken(tokenValue);

            var createToken = new Token
            {
                Value = encryptedTokenValue,
                Role = user.Role,
                UserId = user.Id
            };

            return _tokenRepository.CreateTokenForUser(createToken);
        }

        public bool DeleteTokenByUserId(int? userId)
        {
            var tokenToDelete = _tokenRepository.GetTokenByUserId(userId);
            if (tokenToDelete.UserId == userId)
                return _tokenRepository.DeleteToken(tokenToDelete);

            return true;
        }

        public bool DoesTokenExist(string? value)
        {
            if (value == null)
                return false;
            
            var encryptedValue = TokenHelper.EncryptToken(value);
            return _tokenRepository.DoesTokenExist(encryptedValue);
        }

        public Token GetTokenByValue(string? value)
        {
            if (value == null)
                return new Token();
            
            var encryptedValue = TokenHelper.EncryptToken(value);
            return _tokenRepository.GetTokenByValue(encryptedValue);
        }

        public string GetTokenValueByUserId(int? userId)
        {
            var cryptedToken = _tokenRepository.GetTokenByUserId(userId);
            if (cryptedToken.Value == null)
                return "null";
            
            return TokenHelper.DecryptToken(cryptedToken.Value);
        }
    }
}
