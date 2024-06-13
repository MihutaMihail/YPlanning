using YPlanning.Data;
using YPlanning.Interfaces.Repository;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private DataContext _context;

        public TokenRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTokenForUser(Token createToken)
        {
            _context.Add(createToken);
            return Save();
        }
        
        public bool DeleteToken(Token deleteToken)
        {
            _context.Remove(deleteToken);
            return Save();
        }

        public bool DoesTokenExist(string? value)
        {
            return _context.Tokens?
                .Any(t => t.Value == value) ?? false;
        }

        public Token GetTokenByUserId(int? userId)
        {
            return _context.Tokens?
                .Where(t => t.UserId == userId)
                .FirstOrDefault() ?? new Token();
        }

        public Token GetTokenByValue(string? value)
        {
            return _context.Tokens?
                .Where(t => t.Value == value)
                .FirstOrDefault() ?? new Token();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
