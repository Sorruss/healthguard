using healthguard.Dto;
using healthguard.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IApplicationUserRepository _userAccountRepository;

        public UserAccountController(IApplicationUserRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdministrator(ApplicationUserDto userDto)
        {
            var response = await _userAccountRepository.CreateAdministratorAccount(userDto);
            return Ok(response);
        }

        [HttpPost("register-manager")]
        public async Task<IActionResult> RegisterCompanyManager(
            [FromBody] ApplicationUserDto userDto, 
            [FromQuery] int companyId)
        {
            var response = await _userAccountRepository.CreateCompanyManagerAccount(userDto, companyId);
            return Ok(response);
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor(ApplicationUserDto userDto, int spezId)
        {
            var response = await _userAccountRepository.CreateDoctorAccount(userDto, spezId);
            return Ok(response);
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient(
            [FromQuery] int btypeId,
            [FromQuery] int companyId,
            PatientUserDto userDto)
        {
            var response = await _userAccountRepository.CreatePatientAccount(userDto, btypeId, companyId);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userAccountRepository.LoginAccount(loginDto);
            return Ok(response);
        }
    }
}
