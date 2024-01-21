using static healthguard.Dto.ServiceResponseDto;
using healthguard.Dto;

namespace healthguard.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<GeneralResponse> CreateAccount(UserDto userDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);
    }
}
