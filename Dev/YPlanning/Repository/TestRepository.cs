using YPlanning.Data;
using YPlanning.Interfaces.Repository;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;

        public TestRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTest(Test createTest)
        {
            _context.Add(createTest);
            return Save();
        }

        public bool DeleteTest(Test deleteTest)
        {
            _context.Remove(deleteTest);
            return Save();
        }

        public bool DeleteTests(List<Test> deleteTests)
        {
            _context.RemoveRange(deleteTests);
            return Save();
        }

        public bool DoesTestExistByClassAndUserId(int? classId, int? userId)
        {
            return _context.Tests?
                .Any(t => t.ClassId == classId && t.UserId == userId) ?? false;
        }

        public bool DoesTestExistById(int? id)
        {
            return _context.Tests?
                .Any(t => t.Id == id) ?? false;
        }

        public ICollection<Class> GetClassesByUserId(int? userId)
        {
            return _context.Tests?
                .Where(at => at.UserId == userId)
                .Select(uc => uc.Class)
                .ToList() ?? new List<Class>();
        }

        public Test GetTestByClassAndUserId(int? classId, int? userId)
        {
            return _context.Tests?
                .Where(t => t.ClassId == classId && t.UserId == userId)
                .FirstOrDefault() ?? new Test();
        }

        public Test GetTestById(int? id)
        {
            return _context.Tests?
                .Where(t => t.Id == id)
                .FirstOrDefault() ?? new Test();
        }

        public ICollection<Test> GetTests()
        {
            return _context.Tests?
                .OrderBy(t => t.Id)
                .ToList() ?? new List<Test>();
        }

        public ICollection<Test> GetTestsByClassId(int? classId)
        {
            return _context.Tests?
                .Where(t => t.ClassId == classId)
                .ToList() ?? new List<Test>();
        }

        public ICollection<Test> GetTestsByUserId(int? userId)
        {
            return _context.Tests?
                .Where(t => t.UserId == userId)
                .ToList() ?? new List<Test>();
        }

        public ICollection<User> GetUsersByClassId(int? classId)
        {
            return _context.Tests?
                .Where(at => at.ClassId == classId)
                .Select(uc => uc.User)
                .ToList() ?? new List<User>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTest(Test updatedTest)
        {
            _context.Update(updatedTest);
            return Save();
        }
    }
}
