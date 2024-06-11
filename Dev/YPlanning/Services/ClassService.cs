using YPlanning.Dto;
using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITestService _testService;
        private readonly IAttendanceService _attendanceService;

        public ClassService(IClassRepository classRepository,
            ITestService testService,
            IAttendanceService attendanceService)
        {
            _classRepository = classRepository;
            _testService = testService;
            _attendanceService = attendanceService;
        }

        public bool CreateClass(Class createClass)
        {
            return _classRepository.CreateClass(createClass);
        }

        public bool DeleteClassById(int? id)
        {
            if (!DeleteClassReferences(id))
                return false;
            
            var classToDelete = _classRepository.GetClassById(id);
            return _classRepository.DeleteClass(classToDelete);
        }
        
        public bool DeleteClassesByDate(DateTime? date)
        {
            var classesToDelete = _classRepository.GetClassesByDate(date).ToList();
            if (classesToDelete.Count > 0)
            {
                foreach (var classToDelete in classesToDelete)
                {
                    var classId = classToDelete.Id;
                    if (!DeleteClassReferences(classId))
                        return false;
                }
                return _classRepository.DeleteClasses(classesToDelete);
            }
            return true;
        }
        
        public bool DeleteClassesBySubject(string? subject)
        {
            var classesToDelete = GetClassesBySubject(subject).ToList();
            if (classesToDelete.Count > 0)
            {
                foreach (var classToDelete in classesToDelete)
                {
                    var classId = classToDelete.Id;
                    if (!DeleteClassReferences(classId))
                        return false;
                }
                return _classRepository.DeleteClasses(classesToDelete);
            }
            return true;
        }

        public bool DoesClassDtoExists(ClassDto classCreate)
        {
            classCreate.ClassDate = classCreate.ClassDate?.ToUniversalTime();
            return _classRepository.DoesClassDtoExists(classCreate);
        }

        public bool DoesClassExistsById(int? id)
        {
            return _classRepository.DoesClassExistsById(id);
        }

        public Class GetClassById(int? id)
        {
            return _classRepository.GetClassById(id);
        }

        public ICollection<Class> GetClasses()
        {
            return _classRepository.GetClasses();
        }

        public ICollection<Class> GetClassesByDate(DateTime? date)
        {
            return _classRepository.GetClassesByDate(date);
        }

        public ICollection<Class> GetClassesBySubject(string? subject)
        {
            var trimmedSubject = subject?.Trim().ToUpper() ?? string.Empty;

            return _classRepository.GetClassesBySubject(trimmedSubject);
        }
        
        public bool UpdateClass(Class updatedClass)
        {
            return _classRepository.UpdateClass(updatedClass);
        }
        
        private bool DeleteClassReferences(int? classId)
        {
            if (!_attendanceService.DeleteAttendancesByClassId(classId))
                return false;

            if (!_testService.DeleteTestsByClassId(classId))
                return false;
            
            return true;
        }
    }
}
