using YPlanning.Interfaces.Repository;
using YPlanning.Interfaces.Services;
using YPlanning.Models;
using YPlanning.Repository;

namespace YPlanning.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public bool CreateTest(Test createTest)
        {
            return _testRepository.CreateTest(createTest);
        }

        public bool DeleteTestByClassAndUserId(int? classId, int? userId)
        {
            var testToDelete = _testRepository.GetTestByClassAndUserId(classId, userId);
            return _testRepository.DeleteTest(testToDelete);
        }

        public bool DeleteTestById(int? id)
        {
            var testToDelete = _testRepository.GetTestById(id);
            return _testRepository.DeleteTest(testToDelete);
        }

        public bool DeleteTestsByClassId(int? classId)
        {
            var testsToDelete = _testRepository.GetTestsByClassId(classId).ToList();
            if (testsToDelete.Count > 0)
                return _testRepository.DeleteTests(testsToDelete);

            return true;
        }

        public bool DeleteTestsByUserId(int? userId)
        {
            var testsToDelete = _testRepository.GetTestsByUserId(userId).ToList();
            if (testsToDelete.Count > 0)
                return _testRepository.DeleteTests(testsToDelete);

            return true;
        }

        public bool DoesTestExistByClassAndUserId(int? classId, int? userId)
        {
            return _testRepository.DoesTestExistByClassAndUserId(classId, userId);
        }

        public bool DoesTestExistById(int? id)
        {
            return _testRepository.DoesTestExistById(id);
        }

        public ICollection<Class> GetClassesByUserId(int? userId)
        {
            return _testRepository.GetClassesByUserId(userId);
        }

        public Test GetTestByClassAndUserId(int? classId, int? userId)
        {
            return _testRepository.GetTestByClassAndUserId(classId, userId);
        }

        public Test GetTestById(int? id)
        {
            return _testRepository.GetTestById(id);
        }

        public ICollection<Test> GetTests()
        {
            return _testRepository.GetTests();
        }

        public ICollection<User> GetUsersByClassId(int? classId)
        {
            return _testRepository.GetUsersByClassId(classId);
        }

        public bool UpdateTest(Test updatedTest)
        {
            return _testRepository.UpdateTest(updatedTest);
        }
    }
}
