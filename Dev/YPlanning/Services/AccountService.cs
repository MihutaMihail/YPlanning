using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public bool CreateAccount(Account createAccount)
        {
            return _accountRepository.CreateAccount(createAccount);
        }

        public bool DeleteAccountById(int? id)
        {
            var accountToDelete = _accountRepository.GetAccountById(id);
            return _accountRepository.DeleteAccount(accountToDelete);
        }

        public bool DeleteAccountByUserId(int? userId)
        {
            var accountToDelete = _accountRepository.GetAccountByUserId(userId);
            if (accountToDelete.UserId == userId)
                return _accountRepository.DeleteAccount(accountToDelete);

            return true;
        }

        public bool DoesAccountExistsById(int? id)
        {
            return _accountRepository.DoesAccountExistsById(id);
        }

        public bool DoesAccountExistsByUserId(int? userId)
        {
            return _accountRepository.DoesAccountExistsByUserId(userId);
        }
        
        public Account GetAccountById(int? id)
        {
            return _accountRepository.GetAccountById(id);
        }
        
        public Account GetAccountByUserId(int? userId)
        {
            return _accountRepository.GetAccountByUserId(userId);
        }

        public ICollection<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public bool UpdateAccount(Account updatedAccount)
        {
            return _accountRepository.UpdateAccount(updatedAccount);
        }
    }
}
