using healthguard.Dto;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace healthguard.Repository
{
    public class UserAccountRepository(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration) : IUserAccountRepository
    {
        public async Task<ServiceResponseDto.GeneralResponse> CreateAccount(UserDto userDto)
        {
            if (userDto == null)
                return new ServiceResponseDto.GeneralResponse(false, "Model is empty");

            var newUser = new ApplicationUser()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                UserName = userDto.Email
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
                return new ServiceResponseDto.GeneralResponse(false, "User is already registered");

            var userCreate = await userManager.CreateAsync(newUser, userDto.Password);
            if (!userCreate.Succeeded)
                return new ServiceResponseDto.GeneralResponse(false, "An error occured creating the user");

            var isAdmin = await roleManager.FindByNameAsync("Administrator");
            if (isAdmin == null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Administrator" });
                await userManager.AddToRoleAsync(newUser, "Administrator");
                return new ServiceResponseDto.GeneralResponse(true, "Account created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new ServiceResponseDto.GeneralResponse(true, "Account created");
            }
        }

        public async Task<ServiceResponseDto.LoginResponse> LoginAccount(LoginDto loginDto)
        {
            if (loginDto == null)
                return new ServiceResponseDto.LoginResponse(false, null, "Login is empty");

            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return new ServiceResponseDto.LoginResponse(false, null, "User was not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkUserPasswords)
                return new ServiceResponseDto.LoginResponse(false, null, "Invalid email/password");

            var userRole = await userManager.GetRolesAsync(user);
            var userSession = new UserSessionDto(user.Id, user.Name, user.Email, userRole.First());
            string token = GenerateToken(userSession);

            return new ServiceResponseDto.LoginResponse(true, token, "Login completed");
        }

        private string GenerateToken(UserSessionDto userSession)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.Id),
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["jwt:Issuer"],
                audience: configuration["jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
