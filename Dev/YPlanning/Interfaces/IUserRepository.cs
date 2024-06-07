using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        bool UserExists(int id);
        bool CreateUser(User createUser);
        bool UpdateUser(User updatedUser);
        bool DeleteUser(User deleteUser);
        bool Save();
    }
}
