using YPlanning.Data;
using YPlanning.Interfaces;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) 
        {
            _context = context;            
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users?.OrderBy(u => u.Id).ToList() ?? new List<User>();
        }
    }
}
