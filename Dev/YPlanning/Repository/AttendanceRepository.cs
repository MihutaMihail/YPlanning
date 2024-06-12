using Microsoft.EntityFrameworkCore;
using YPlanning.Data;
using YPlanning.Interfaces.Repository;
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

        public bool CreateAttendance(Attendance createAttendance)
        {
            _context.Add(createAttendance);
            return Save();
        }

        public bool DeleteAttendance(Attendance deleteAttendance)
        {
            _context.Remove(deleteAttendance);
            return Save();
        }

        public bool DeleteAttendances(List<Attendance> deleteAttendances)
        {
            _context.RemoveRange(deleteAttendances);
            return Save();
        }

        public bool DoesAttendanceExistByClassAndUserId(int? classId, int? userId)
        {
            return _context.Attendances?
               .Any(at => at.ClassId == classId && at.UserId == userId) ?? false;
        }

        public bool DoesAttendanceExistById(int? id)
        {
            return _context.Attendances?
                .Any(at => at.Id == id) ?? false;
        }

        public Attendance GetAttendanceByClassAndUserId(int? classId, int? userId)
        {
            return _context.Attendances?
                 .Where(at => at.ClassId == classId && at.UserId == userId)
                 .FirstOrDefault() ?? new Attendance();
        }
        
        public Attendance GetAttendanceById(int? id)
        {
            return _context.Attendances?
                .Where(at => at.Id == id)
                .FirstOrDefault() ?? new Attendance();
        }

        public ICollection<Attendance> GetAttendances()
        {
            return _context.Attendances?
                .Include(at => at.User)
                .Include(at => at.Class)
                .OrderBy(at => at.UserId)
                .ToList() ?? new List<Attendance>();
        }

        public ICollection<Attendance> GetAttendancesByClassId(int? classId)
        {
            return _context.Attendances?
                .Where(at => at.ClassId == classId)
                .ToList() ?? new List<Attendance>();
        }

        public ICollection<Attendance> GetAttendancesByUserId(int? userId)
        {
            return _context.Attendances?
                .Where(at => at.UserId == userId)
                .ToList() ?? new List<Attendance>();
        }

        public ICollection<Class> GetClassesByUserId(int? userId)
        {
            return _context.Attendances?
                .Where(at => at.UserId == userId)
                .Select(uc => uc.Class)
                .ToList() ?? new List<Class>();
        }

        public ICollection<User> GetUsersByClassId(int? classId)
        {
            return _context.Attendances?
                .Where(at => at.ClassId == classId)
                .Select(uc => uc.User)
                .ToList() ?? new List<User>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAttendance(Attendance updatedAttendance)
        {
            _context.Update(updatedAttendance);
            return Save();
        }
    }
}
