using static healthguard.Dto.ServiceResponseDto;
using healthguard.Dto;

namespace healthguard.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<GeneralResponse> CreateAdministratorAccount(ApplicationUserDto userDto);
        Task<GeneralResponse> CreateCompanyManagerAccount(ApplicationUserDto userDto, int companyId);
        Task<GeneralResponse> CreateDoctorAccount(ApplicationUserDto userDto, int spezId);
        Task<GeneralResponse> CreatePatientAccount(PatientUserDto userDto, int btypeId, int companyId);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);
    }
}
