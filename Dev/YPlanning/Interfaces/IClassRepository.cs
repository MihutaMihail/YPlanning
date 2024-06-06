using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IClassRepository
    {
        ICollection<Class> GetClasses();
        Class GetClassById(int id);
        bool ClassExists(int id);
        bool CreateClass(Class classCreate);
        bool Save();
    }
}
