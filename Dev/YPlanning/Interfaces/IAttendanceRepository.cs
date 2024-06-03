using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface IAttendanceRepository
    {
        ICollection<Attendance> GetAttendances();
        ICollection<User> GetUsersByClassId(int classId);
        ICollection<Class> GetClassesByUserId(int userId);
        bool AttedanceExists(int classId, int userId);
    }
}
