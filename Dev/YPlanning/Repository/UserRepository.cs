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
            return _context.Users?
                .OrderBy(u => u.Id)
                .ToList() ?? new List<User>();
        }

        public User GetUserById(int id)
        {
            return _context.Users?
                .Where(u => u.Id == id)
                .FirstOrDefault() ?? new User();
        }

        public bool UserExists(int id)
        {
            return _context.Users?
                .Any(u => u.Id == id) ?? false;
        }
    }
}
