using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        User GetUserById(int? id);
        User GetUserByName(string? lastName, string? firstName);
        bool CreateUser(User createUser);
        bool UpdateUser(User updatedUser);
        bool DoesUserDtoExists(UserDto userCreate);
        bool DeleteUserById(int? id);
        bool DeleteUserByName(string? lastName, string? firstName);
        bool DoesUserExistById(int? id);
        bool DoesUserExistByName(string? lastName, string? firstName);
    }
}
