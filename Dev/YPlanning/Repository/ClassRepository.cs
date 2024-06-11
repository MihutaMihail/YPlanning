using YPlanning.Data;
using YPlanning.Dto;
using YPlanning.Interfaces.Repository;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateClass(Class createClass)
        {
            _context.Add(createClass);
            return Save();
        }

        public bool DeleteClass(Class deleteClass)
        {
            _context.Remove(deleteClass);
            return Save();
        }

        public bool DeleteClasses(List<Class> deleteClasses)
        {
            _context.RemoveRange(deleteClasses);
            return Save();
        }

        public bool DoesClassDtoExists(ClassDto classCreate)
        {
            return _context.Classes?
                .Any(c =>
                    c.ClassDate == classCreate.ClassDate &&
                    c.StartTime == classCreate.StartTime &&
                    c.EndTime == classCreate.EndTime &&
                    c.Room == classCreate.Room) ?? false;
        }

        public bool DoesClassExistsById(int? id)
        {
            return _context.Classes?
               .Any(c => c.Id == id) ?? false;
        }
        
        public Class GetClassById(int? id)
        {
            return _context.Classes?
                .Where(c => c.Id == id)
                .FirstOrDefault() ?? new Class();
        }

        public ICollection<Class> GetClasses()
        {
            return _context.Classes?
                .OrderBy(c => c.Id)
                .ToList() ?? new List<Class>();
        }

        public ICollection<Class> GetClassesByDate(DateTime? date)
        {
            return _context.Classes?
                .Where(c => c.ClassDate == date)
                .OrderBy(c => c.Id)
                .ToList() ?? new List<Class>();
        }

        public ICollection<Class> GetClassesBySubject(string? subject)
        {
            return _context.Classes?
                .Where(c => c.Subject != null && c.Subject.Trim().ToUpper() == subject)
                .OrderBy(c => c.Id)
                .ToList() ?? new List<Class>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClass(Class updatedClass)
        {
            _context.Update(updatedClass);
            return Save();
        }
    }
}
