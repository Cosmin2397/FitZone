using FitZone.AuthService.Dtos;
using FitZone.AuthService.Entities;
using FitZone.AuthService.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FitZone.AuthService.Repositories
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthentificationRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<LoginResponse> LoginUser(LoginModel user)
        {
            var response = new LoginResponse();
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                return response;
            }
            if (await _userManager.CheckPasswordAsync(identityUser, user.Password))
            {
                var roles = await _userManager.GetRolesAsync(identityUser);
                response.JwtToken = this.GenerateToken(user.Email, (List<string>)roles, true, identityUser.Id.ToString(),identityUser.GymId);
                response.RefreshToken = this.GenerateRefreshToken();
                identityUser.RefreshToken = response.RefreshToken;
                identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(5);
                await _userManager.UpdateAsync(identityUser);
                return response;
            }

            return response;
        }

        public async Task<bool> LogoutUser(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                return false;
            }

            identityUser.RefreshToken = string.Empty;
            identityUser.RefreshTokenExpiry = DateTime.MinValue;

            var result = await _userManager.UpdateAsync(identityUser);

            return result.Succeeded;
        }


        public async Task<bool> RegisterUser(RegisterModel register)
        {
            var user = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PhoneNumber = register.PhoneNumber,
                GymId = register.GymId
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            string roleName = register.IsEmployee ? register.RoleName.ToString() : Role.Client.ToString();

            // Verifică dacă rolul există
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            // Adaugă rolul utilizatorului
            var rolesResult =  await _userManager.AddToRoleAsync(user, roleName);

            if (register.IsEmployee && register.EmployeeData != null)
            {
                //Verifica daca rolul exista deja in baza de date, dupa nume.
                //Daca nu exista il adauga si adauga in tabela de users role rolul asociat contului
                //Trimite eveniment la employee api pentru a adauga angajatul cu datele lui
            }
                return result.Succeeded && rolesResult.Succeeded;
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenModel model)
        {
            var principal = GetTokenPrincipal(model.JwtToken);

            var response = new LoginResponse();
            if (principal?.Identity?.Name is null)
            {
                return response;
            }

            var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry > DateTime.Now)
            {
                return response;
            }

            var roles = await _userManager.GetRolesAsync(identityUser);
            response.JwtToken = this.GenerateToken(identityUser.Email, (List<string>)roles, true, identityUser.Id.ToString(), identityUser.GymId);
            response.RefreshToken = this.GenerateRefreshToken();
            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(5);
            await _userManager.UpdateAsync(identityUser);
            return response;


        }

        private ClaimsPrincipal? GetTokenPrincipal(string token)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateToken(string userName, List<string> roles, bool isLogged, string id, Guid? gymId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Sid,id),
                new Claim(ClaimTypes.Authentication,isLogged.ToString()),
                new Claim(ClaimTypes.GroupSid,gymId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var staticKey = _configuration.GetSection("Jwt:Key").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(staticKey));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            List<UserDto> responseUsers = new List<UserDto>(); 
            foreach (var user in users)
            {
                var userDto = new UserDto();
                userDto.ConvertFromApplicationUser(user);
                userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
                responseUsers.Add(userDto);
            }

            return responseUsers;
        }

        public async Task<List<UserDto>> GetGymUsers(Guid gymId)
        {
            var users = _userManager.Users.Where(g=> g.GymId == gymId).ToList();
            List<UserDto> responseUsers = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = new UserDto();
                userDto.ConvertFromApplicationUser(user);
                userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
                responseUsers.Add(userDto);
            }

            return responseUsers;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            var userDto = new UserDto();
            userDto.ConvertFromApplicationUser(user);
            userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            return userDto;
        }


        public async Task<bool> DeleteUser(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            //Produce eveniment de stergere utilizator pentu employee api si schedule api, subscription api, calorie tracker service
            return result.Succeeded;
        }

        public async Task<bool> UpdateUser(UpdateUser newUser)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == newUser.Id);

            if (user == null)
            {
                return false;
            }

            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.PhoneNumber = newUser.PhoneNumber;
  
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UpdateUserGym(Guid userId, Guid newGym)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.GymId = newGym;
            var result = await _userManager.UpdateAsync(user);

            //Produce eveniment pentru a sterge abonamentele active, programarile

            return result.Succeeded;
        }

    }

}
