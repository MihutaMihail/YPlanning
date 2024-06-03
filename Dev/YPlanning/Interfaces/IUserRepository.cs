using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        bool UserExists(int id);
    }
}
