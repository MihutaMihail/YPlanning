using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IClassRepository
    {
        ICollection<Class> GetClasses();
        Class GetClassById(int id);
        bool ClassExists(int id);
        bool CreateClass(Class createClass);
        bool UpdateClass(Class updatedClass);
        bool DeleteClass(Class deleteClass);
        bool Save();
    }
}
