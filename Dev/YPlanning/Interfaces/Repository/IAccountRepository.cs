using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface IAccountRepository
    {
        bool Authenticate(string? login, string? password);
        ICollection<Account> GetAccounts();
        Account GetAccountById(int? id);
        Account GetAccountByUserId(int? userId);
        DateTime GetAccountCreationDateById(int? id);
        Account GetAccountByLogin(string? login);
        bool CreateAccount(Account createAccount);
        bool UpdateAccount(Account updatedAccount);
        bool DeleteAccount(Account deleteAccount);
        bool DoesAccountExistsById(int? id);
        bool DoesAccountExistsByUserId(int? userId);
        bool DoesAccountExistsByLogin(string? login);
        bool Save();
    }
}
