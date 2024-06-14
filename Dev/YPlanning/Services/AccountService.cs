using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountService(IAccountRepository accountRepository,
            IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public bool Authenticate(string? login, string? password)
        {
            return _accountRepository.Authenticate(login, password);
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

        public bool DoesAccountExistsByLogin(string? login)
        {
            return _accountRepository.DoesAccountExistsByLogin(login);
        }

        public bool DoesAccountExistsByUserId(int? userId)
        {
            return _accountRepository.DoesAccountExistsByUserId(userId);
        }
        
        public Account GetAccountById(int? id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public Account GetAccountByLogin(string? login)
        {
            return _accountRepository.GetAccountByLogin(login);
        }

        public Account GetAccountByUserId(int? userId)
        {
            return _accountRepository.GetAccountByUserId(userId);
        }

        public DateTime GetAccountCreationDateById(int? id) 
        {
            return _accountRepository.GetAccountCreationDateById(id);
        }

        public ICollection<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public User GetUserByAccount(string? login, string? password)
        {
            if (DoesAccountExistsByLogin(login))
            {
                var account = GetAccountByLogin(login);
                var user = _userRepository.GetUserById(account.UserId);
                return user;
            }

            return new User();
        }

        public bool UpdateAccount(Account updatedAccount)
        {
            return _accountRepository.UpdateAccount(updatedAccount);
        }
    }
}
