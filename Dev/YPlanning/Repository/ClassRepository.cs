﻿using YPlanning.Data;
using YPlanning.Interfaces;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Class> GetClasses()
        {
            return _context.Classes?
                .OrderBy(c => c.Id)
                .ToList() ?? new List<Class>();
        }

        public Class GetClassById(int id)
        {
            return _context.Classes?
                .Where(c => c.Id == id)
                .FirstOrDefault() ?? new Class();
        }

        public bool ClassExists(int id)
        {
            return _context.Classes?
                .Any(c => c.Id == id) ?? false;
        }
    }
}