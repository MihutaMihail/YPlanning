using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public bool CreateAttendance(Attendance createAttendance)
        {
            return _attendanceRepository.CreateAttendance(createAttendance);
        }
        
        public bool DeleteAttendanceByClassAndUserId(int? classId, int? userId)
        {
            var attendanceToDelete = _attendanceRepository.GetAttendanceByClassAndUserId(classId, userId);
            return _attendanceRepository.DeleteAttendance(attendanceToDelete);
        }
        
        public bool DeleteAttendancesByClassId(int? classId)
        {
            var attendancesToDelete = _attendanceRepository.GetAttendancesByClassId(classId).ToList();
            if (attendancesToDelete.Count > 0)
                return _attendanceRepository.DeleteAttendances(attendancesToDelete);

            return true;
        }

        public bool DeleteAttendanceById(int? id)
        {
            var attendanceToDelete = _attendanceRepository.GetAttendanceById(id);
            return _attendanceRepository.DeleteAttendance(attendanceToDelete);
        }

        public bool DeleteAttendancesByUserId(int? userId)
        {
            var attendancesToDelete = _attendanceRepository.GetAttendancesByUserId(userId).ToList();
            if (attendancesToDelete.Count > 0)
                return _attendanceRepository.DeleteAttendances(attendancesToDelete);

            return true;
        }

        public bool DoesAttendanceExistByClassAndUserId(int? classId, int? userId)
        {
            return _attendanceRepository.DoesAttendanceExistByClassAndUserId(classId, userId);
        }

        public bool DoesAttendanceExistById(int? id)
        {
            return _attendanceRepository.DoesAttendanceExistById(id);
        }

        public Attendance GetAttendanceByClassAndUserId(int? classId, int? userId)
        {
            return _attendanceRepository.GetAttendanceByClassAndUserId(classId, userId);
        }
        
        public Attendance GetAttendanceById(int? id)
        {
            return _attendanceRepository.GetAttendanceById(id);
        }

        public ICollection<Attendance> GetAttendances()
        {
            return _attendanceRepository.GetAttendances();
        }

        public ICollection<Class> GetClassesByUserId(int? userId)
        {
            return _attendanceRepository.GetClassesByUserId(userId);
        }

        public ICollection<User> GetUsersByClassId(int? classId)
        {
            return _attendanceRepository.GetUsersByClassId(classId);
        }

        public bool UpdateAttendance(Attendance updatedAttendance)
        {
            return _attendanceRepository.UpdateAttendance(updatedAttendance);
        }
    }
}
