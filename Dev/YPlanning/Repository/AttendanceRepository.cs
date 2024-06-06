using Microsoft.EntityFrameworkCore;
using YPlanning.Data;
using YPlanning.Interfaces;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DataContext _context;

        public AttendanceRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Attendance> GetAttendances()
        {
            return _context.Attendances?
                .Include(at => at.User)
                .Include(at => at.Class)
                .OrderBy(at => at.UserId)
                .ToList() ?? new List<Attendance>();
        }

        public ICollection<Class> GetClassesByUserId(int userId)
        {
            return _context.Attendances?
                .Where(at => at.UserId == userId)
                .Select(uc => uc.Class)
                .ToList() ?? new List<Class>();
        }

        public ICollection<User> GetUsersByClassId(int classId)
        {
            return _context.Attendances?
                .Where(at => at.ClassId == classId)
                .Select(uc => uc.User)
                .ToList() ?? new List<User>();
        }

        public bool AttendanceExists(int classId, int userId)
        {
            return _context.Attendances?.Any(uc => uc.UserId == userId && uc.ClassId == classId) ?? false;

        }

        public bool CreateAttendance(Attendance attendanceCreate)
        {
            _context.Add(attendanceCreate);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
