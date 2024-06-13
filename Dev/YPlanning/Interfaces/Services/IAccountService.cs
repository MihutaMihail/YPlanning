using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface IAccountService
    {
        bool Authenticate(string? login, string? password);
        ICollection<Account> GetAccounts();
        User GetUserByAccount(string? login, string? password);
        Account GetAccountById(int? id);
        Account GetAccountByUserId(int? userId);
        DateTime GetAccountCreationDateById(int? id);
        Account GetAccountByLogin(string? login);
        bool CreateAccount(Account createAccount);
        bool UpdateAccount(Account updatedAccount);
        bool DeleteAccountById(int? id);
        bool DeleteAccountByUserId(int? userId);
        bool DoesAccountExistsById(int? id);
        bool DoesAccountExistsByUserId(int? id);
        bool DoesAccountExistsByLogin(string? login);
    }
}
