using Microsoft.AspNetCore.Identity;
using YPlanning.Data;
using YPlanning.Interfaces.Repository;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;


        public AccountRepository(DataContext context, IPasswordHasher<Account> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public bool Authenticate(string? login, string? password)
        {
            var account = _context.Accounts?
                .Where(ac => ac.Login == login)
                .FirstOrDefault() ?? null;

            if (account == null)
                return false;

            var verificationResult = _passwordHasher.VerifyHashedPassword(account, account.Password, password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        public bool CreateAccount(Account createAccount)
        {
            _context.Add(createAccount);
            return Save();
        }

        public bool DeleteAccount(Account deleteAccount)
        {
            _context.Remove(deleteAccount);
            return Save();
        }

        public bool DoesAccountExistsById(int? id)
        {
            return _context.Accounts?
                .Any(ac => ac.Id == id) ?? false;
        }

        public bool DoesAccountExistsByLogin(string? login)
        {
            return _context.Accounts?
                .Any(ac => ac.Login == login) ?? false;
        }

        public bool DoesAccountExistsByUserId(int? userId)
        {
            return _context.Accounts?
                .Any(ac => ac.UserId == userId) ?? false;
        }

        public Account GetAccountById(int? id)
        {
            return _context.Accounts?
                .Where(ac => ac.Id == id)
                .FirstOrDefault() ?? new Account();
        }

        public Account GetAccountByLogin(string? login)
        {
            return _context.Accounts?
                .Where(ac => ac.Login == login)
                .FirstOrDefault() ?? new Account();
        }

        public Account GetAccountByUserId(int? userId)
        {
            return _context.Accounts?
                .Where(ac => ac.UserId == userId)
                .FirstOrDefault() ?? new Account();
        }

        public DateTime GetAccountCreationDateById(int? id)
        {
            return _context.Accounts?
                .Where(ac => ac.Id == id)
                .Select(ac => ac.AccountCreationDate)
                .FirstOrDefault() ?? new DateTime();
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts?
                .OrderBy(ac => ac.Id)
                .ToList() ?? new List<Account>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccount(Account updatedAccount)
        {
            _context.Update(updatedAccount);
            return Save();
        }
    }
}
