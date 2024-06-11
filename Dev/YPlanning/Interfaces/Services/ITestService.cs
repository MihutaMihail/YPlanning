using YPlanning.Models;

namespace YPlanning.Interfaces.Services
{
    public interface ITestService
    {
        ICollection<Test> GetTests();
        Test GetTestById(int? id);
        Test GetTestByClassAndUserId(int? classId, int? userId);
        ICollection<User> GetUsersByClassId(int? classId);
        ICollection<Class> GetClassesByUserId(int? userId);
        bool CreateTest(Test createTest);
        bool UpdateTest(Test updatedTest);
        bool DeleteTestById(int? id);
        bool DeleteTestsByClassId(int? classId);
        bool DeleteTestsByUserId(int? userId);
        bool DeleteTestByClassAndUserId(int? classId, int? userId);
        bool DoesTestExistById(int? id);
        bool DoesTestExistByClassAndUserId(int? classId, int? userId);
    }
}
