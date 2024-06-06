using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface ITestRepository
    {
        ICollection<Test> GetTests();
        Test GetTestById(int id);
        bool TestExists(int id);
        bool CreateTest(Test testCreate);
        bool Save();
    }
}
