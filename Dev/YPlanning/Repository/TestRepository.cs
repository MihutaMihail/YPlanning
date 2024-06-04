﻿using YPlanning.Data;
using YPlanning.Interfaces;
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

        public ICollection<Test> GetTests()
        {
            return _context.Tests?
                .OrderBy(t => t.Id)
                .ToList() ?? new List<Test>();
        }

        public Test GetTestById(int id)
        {
            return _context.Tests?
                .Where(t => t.Id == id)
                .FirstOrDefault() ?? new Test();
        }

        public bool TestExists(int id)
        {
            return _context.Tests?
                .Any(t => t.Id == id) ?? false;
        }
    }
}