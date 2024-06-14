using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface IClassRepository
    {
        ICollection<Class> GetClasses();
        Class GetClassById(int? id);
        ICollection<Class> GetClassesBySubject(string? subject);
        ICollection<Class> GetClassesByDate(DateTime? date);
        bool CreateClass(Class createClass);
        bool UpdateClass(Class updatedClass);
        bool DeleteClass(Class deleteClass);
        bool DeleteClasses(List<Class> deleteClasses);
        bool DoesClassDtoExists(ClassDto classCreate);
        bool DoesClassExistsById(int? id);
        bool Save();
    }
}
