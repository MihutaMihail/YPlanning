using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}
