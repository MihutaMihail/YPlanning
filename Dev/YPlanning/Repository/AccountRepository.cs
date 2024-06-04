﻿using YPlanning.Data;
using YPlanning.Interfaces;
using YPlanning.Models;

namespace YPlanning.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts?
                .OrderBy(ac => ac.Id)
                .ToList() ?? new List<Account>();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts?
                .Where(ac => ac.Id == id)
                .FirstOrDefault() ?? new Account();
        }

        public bool AccountExists(int id)
        {
            return _context.Accounts?
                .Any(ac => ac.Id == id) ?? false;
        }
    }
}