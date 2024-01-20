﻿using healthguard.Data;
using healthguard.Interfaces;
using healthguard.Models;

namespace healthguard.Repository
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly DataContext _context;

        public AdministratorRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Administrator> GetAdministrators()
        {
            return _context.Administrators.OrderBy(e => e.AdministratorId).ToList();
        }

        public Administrator GetAdministrator(int adminId)
        {
            return _context.Administrators.Where(e => e.AdministratorId == adminId).FirstOrDefault();
        }

        public bool AdministratorExists(int adminId)
        {
            return _context.Administrators.Any(e => e.AdministratorId == adminId);
        }

        public bool CreateAdministrator(Administrator administrator)
        {
            _context.Add(administrator);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateAdministrator(Administrator administrator)
        {
            _context.Update(administrator);
            return Save();
        }

        public bool DeleteAdministrator(Administrator administrator)
        {
            _context.Remove(administrator);
            return Save();
        }
    }
}
