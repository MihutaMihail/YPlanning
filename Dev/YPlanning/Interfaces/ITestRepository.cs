﻿using YPlanning.Models;

namespace YPlanning.Interfaces
{
    public interface ITestRepository
    {
        ICollection<Test> GetTests();
        Test GetTestById(int id);
        bool TestExists(int id);
        bool CreateTest(Test createTest);
        bool UpdateTest(Test updatedTest);
        bool DeleteTest(Test deleteTest);
        bool Save();
    }
}
