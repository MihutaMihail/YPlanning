using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface IAccountService
    {
        ICollection<Account> GetAccounts();
        Account GetAccountById(int? id);
        Account GetAccountByUserId(int? userId);
        DateTime GetAccountCreationDateById(int? id);
        bool CreateAccount(Account createAccount);
        bool UpdateAccount(Account updatedAccount);
        bool DeleteAccountById(int? id);
        bool DeleteAccountByUserId(int? userId);
        bool DoesAccountExistsById(int? id);
        bool DoesAccountExistsByUserId(int? id);
    }
}
