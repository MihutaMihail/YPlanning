using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface IClassService
    {
        ICollection<Class> GetClasses();
        Class GetClassById(int? id);
        ICollection<Class> GetClassesBySubject(string? subject);
        ICollection<Class> GetClassesByDate(DateTime? date);
        bool CreateClass(Class createClass);
        bool UpdateClass(Class updatedClass);
        bool DeleteClassById(int? id);
        bool DeleteClassesBySubject(string? subject);
        bool DeleteClassesByDate(DateTime? date);
        bool DoesClassDtoExists(ClassDto classCreate);
        bool DoesClassExistsById(int? id);
    }
}
