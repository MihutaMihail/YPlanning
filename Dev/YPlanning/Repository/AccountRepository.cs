using YPlanning.Data;
using YPlanning.Interfaces.Repository;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
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
        
        public Account GetAccountByUserId(int? userId)
        {
            return _context.Accounts?
                .Where(ac => ac.UserId == userId)
                .FirstOrDefault() ?? new Account();
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
