using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccountById(int? id);
        Account GetAccountByUserId(int? userId);
        bool CreateAccount(Account createAccount);
        bool UpdateAccount(Account updatedAccount);
        bool DeleteAccount(Account deleteAccount);
        bool DoesAccountExistsById(int? id);
        bool DoesAccountExistsByUserId(int? userId);
        bool Save();
    }
}
