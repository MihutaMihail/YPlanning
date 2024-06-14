using YPlanning.Dto;
using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestService _testService;
        private readonly IAttendanceService _attendanceService;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository,
            ITestService testService,
            IAttendanceService attendanceService,
            IAccountService accountService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _testService = testService;
            _attendanceService = attendanceService;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        public bool CreateUser(User createUser)
        {
            return _userRepository.CreateUser(createUser);
        }

        public bool DeleteUserById(int? id)
        {
            if (!DeleteUserReferences(id))
                return false;

            var userToDelete = _userRepository.GetUserById(id);
            return _userRepository.DeleteUser(userToDelete);
        }

        public bool DeleteUserByName(string? lastName, string? firstName)
        {
            var userToDelete = GetUserByName(lastName, firstName);
            if (!DeleteUserReferences(userToDelete.Id))
                return false;

            return _userRepository.DeleteUser(userToDelete);
        }

        public bool DoesUserDtoExists(UserDto userCreate)
        {
            return _userRepository.DoesUserDtoExists(userCreate);
        }

        public bool DoesUserExistById(int? id)
        {
            return _userRepository.DoesUserExistById(id);
        }

        public bool DoesUserExistByName(string? lastName, string? firstName)
        {
            var trimmedLastName = lastName?.Trim().ToUpper() ?? string.Empty;
            var trimmedFirstName = firstName?.Trim().ToUpper() ?? string.Empty;

            return _userRepository.DoesUserExistByName(trimmedLastName, trimmedFirstName);
        }

        public User GetUserById(int? id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByName(string? lastName, string? firstName)
        {
            var trimmedLastName = lastName?.Trim().ToUpper() ?? string.Empty;
            var trimmedFirstName = firstName?.Trim().ToUpper() ?? string.Empty;

            return _userRepository.GetUserByName(trimmedLastName, trimmedFirstName);
        }

        public ICollection<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool UpdateUser(User updatedUser)
        {
            return _userRepository.UpdateUser(updatedUser);
        }

        private bool DeleteUserReferences(int? userId)
        {
            if (!_attendanceService.DeleteAttendancesByUserId(userId))
                return false;

            if (!_testService.DeleteTestsByUserId(userId))
                return false;

            if (!_accountService.DeleteAccountByUserId(userId))
                return false;

            if (!_tokenService.DeleteTokenByUserId(userId))
                return false;
            
            return true;
        }
    }
}
