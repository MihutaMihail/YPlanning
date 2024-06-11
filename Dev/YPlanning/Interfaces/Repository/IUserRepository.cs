using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int? id);
        User GetUserByName(string? lastName, string? firstName);
        bool CreateUser(User createUser);
        bool UpdateUser(User updatedUser);
        bool DeleteUser(User deleteUser);
        bool DoesUserDtoExists(UserDto userCreate);
        bool DoesUserExistById(int? id);
        bool DoesUserExistByName(string? lastName, string? firstName);
        bool Save();
    }
}
