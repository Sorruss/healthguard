using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface ICompanyManagerRepository
    {
        ICollection<CompanyManager> GetCompanyManagers();
        CompanyManager GetCompanyManager(int managerId);
        bool CompanyManagerExists(int managerId);
        bool CreateCompanyManager(CompanyManager companyManager);
        bool UpdateCompanyManager(CompanyManager companyManager);
        bool DeleteCompanyManager(CompanyManager companyManager);
        bool Save();
    }
}
