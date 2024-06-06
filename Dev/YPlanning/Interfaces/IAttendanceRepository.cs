using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IAttendanceRepository
    {
        ICollection<Attendance> GetAttendances();
        ICollection<User> GetUsersByClassId(int classId);
        ICollection<Class> GetClassesByUserId(int userId);
        bool AttendanceExists(int classId, int userId);
        bool CreateAttendance(Attendance attendanceCreate);
        bool Save();
    }
}
