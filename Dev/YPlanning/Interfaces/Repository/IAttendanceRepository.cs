using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface IAttendanceRepository
    {
        ICollection<Attendance> GetAttendances();
        Attendance GetAttendanceById(int? id);
        Attendance GetAttendanceByClassAndUserId(int? classId, int? userId);
        ICollection<Attendance> GetAttendancesByClassId(int? classId);
        ICollection<Attendance> GetAttendancesByUserId(int? userId);
        ICollection<User> GetUsersByClassId(int? classId);
        ICollection<Class> GetClassesByUserId(int? userId);
        bool CreateAttendance(Attendance createAttendance);
        bool UpdateAttendance(Attendance updatedAttendance);
        bool DeleteAttendance(Attendance deleteAttendance);
        bool DeleteAttendances(List<Attendance> deleteAttendances);
        bool DoesAttendanceExistById(int? id);
        bool DoesAttendanceExistByClassAndUserId(int? classId, int? userId);
        bool Save();
    }
}
