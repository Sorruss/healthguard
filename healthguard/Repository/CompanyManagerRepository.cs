﻿using healthguard.Data;
using healthguard.Interfaces;
using healthguard.Models;

namespace healthguard.Repository
{
    public class CompanyManagerRepository : ICompanyManagerRepository
    {
        private readonly DataContext _context;

        public CompanyManagerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CompanyManagerExists(int managerId)
        {
            return _context.CompanyManagers.Any(e => e.CompanyManagerId == managerId);
        }

        public bool CreateCompanyManager(CompanyManager companyManager)
        {
            _context.Add(companyManager);
            return Save();
        }

        public bool DeleteCompanyManager(CompanyManager companyManager)
        {
            _context.Remove(companyManager);
            return Save();
        }

        public CompanyManager GetCompanyManager(int managerId)
        {
            return _context.CompanyManagers.Where(e => e.CompanyManagerId == managerId).FirstOrDefault();
        }

        public ICollection<CompanyManager> GetCompanyManagers()
        {
            return _context.CompanyManagers.OrderBy(e => e.CompanyManagerId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCompanyManager(CompanyManager companyManager)
        {
            _context.Update(companyManager);
            return Save();
        }
    }
}
