using System.Xml.Linq;
using YPlanning.Data;
using YPlanning.Dto;
using YPlanning.Interfaces.Repository;
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

        public bool CreateUser(User createUser)
        {
            _context.Add(createUser);
            return Save();
        }

        public bool DeleteUser(User deleteUser)
        {
            _context.Remove(deleteUser);
            return Save();
        }

        public bool DoesUserDtoExists(UserDto userCreate)
        {
            if (userCreate?.Email == null)
                return false;
            
            var trimmedEmail = userCreate.Email.Trim().ToUpper();

            return _context.Users?
                .Any(u => u.Email != null && u.Email.Trim().ToUpper() == trimmedEmail) ?? false;
        }

        public bool DoesUserExistById(int? id)
        {
            return _context.Users?
                .Any(u => u.Id == id) ?? false;
        }

        public bool DoesUserExistByName(string? lastName, string? firstName)
        {
            return _context.Users?
            .Any(u => u.LastName != null && u.LastName.Trim().ToUpper() == lastName &&
            u.FirstName != null && u.FirstName.Trim().ToUpper() == firstName) ?? false;
        }

        public User GetUserById(int? id)
        {
            return _context.Users?
                .Where(u => u.Id == id)
                .FirstOrDefault() ?? new User();
        }
        
        public User GetUserByName(string? lastName, string? firstName)
        {
            return _context.Users?
                .Where(u => 
                u.LastName != null && u.LastName.Trim().ToUpper() == lastName &&
                u.FirstName != null && u.FirstName.Trim().ToUpper() == firstName)
                .FirstOrDefault() ?? new User();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users?
                .OrderBy(u => u.Id)
                .ToList() ?? new List<User>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User updatedUser)
        {
            _context.Update(updatedUser);
            return Save();
        }
    }
}
