using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccountById(int id);
        bool AccountExists(int id);
        bool CreateAccount(Account newAccount);
        bool Save();
    }
}
