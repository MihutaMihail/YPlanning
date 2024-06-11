using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface IAttendanceService
    {
        ICollection<Attendance> GetAttendances();
        Attendance GetAttendanceById(int? id);
        Attendance GetAttendanceByClassAndUserId(int? classId, int? userId);
        ICollection<User> GetUsersByClassId(int? classId);
        ICollection<Class> GetClassesByUserId(int? userId);
        bool CreateAttendance(Attendance createAttendance);
        bool UpdateAttendance(Attendance updatedAttendance);
        bool DeleteAttendanceById(int? id);
        bool DeleteAttendancesByClassId(int? classId);
        bool DeleteAttendancesByUserId(int? userId);
        bool DeleteAttendanceByClassAndUserId(int? classId, int? userId);
        bool DoesAttendanceExistById(int? id);
        bool DoesAttendanceExistByClassAndUserId(int? classId, int? userId);
    }
}
