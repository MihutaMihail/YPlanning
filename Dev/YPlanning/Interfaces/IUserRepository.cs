using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        // User GetUser(Account account);
        bool UserExists(int id);
    }
}
