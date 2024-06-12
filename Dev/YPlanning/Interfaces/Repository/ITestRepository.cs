using YPlanning.Models;

namespace YPlanning.Interfaces.Repository
{
    public interface ITestRepository
    {
        ICollection<Test> GetTests();
        Test GetTestById(int? id);
        Test GetTestByClassAndUserId(int? classId, int? userId);
        ICollection<Test> GetTestsByClassId(int? classId);
        ICollection<Test> GetTestsByUserId(int? userId);
        ICollection<User> GetUsersByClassId(int? classId);
        ICollection<Class> GetClassesByUserId(int? userId);
        bool CreateTest(Test createTest);
        bool UpdateTest(Test updatedTest);
        bool DeleteTest(Test deleteTest);
        bool DeleteTests(List<Test> deleteTests);
        bool DoesTestExistById(int? id);
        bool DoesTestExistByClassAndUserId(int? classId, int? userId);
        bool Save();
    }
}
