using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface IAdministratorRepository
    {
        ICollection<Administrator> GetAdministrators();
        Administrator GetAdministrator(int id);
        bool AdministratorExists(int id);
        bool CreateAdministrator(Administrator administrator);
        bool UpdateAdministrator(Administrator administrator);
        bool DeleteAdministrator(Administrator administrator);
        bool Save();
    }
}
