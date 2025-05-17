using FitZone.Client.Shared.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<LoginResponseDto> Login(LoginRequestDto user);

        Task<string> Register(RegisterModel user);

        Task<bool> Logout(string email);

        Task<LoginResponseDto> RefreshToken(RefreshTokenDto model);

        Task<List<User>> GetGymUsers(Guid gymId);

        Task<UserDto> GetUserByEmail(string email);

        Task<UserDto> GetUserById(Guid id);

        Task<bool> DeleteUser(string email);

        Task<bool> UpdateUser(UserDto user);

        Task<string> GetUserName(Guid clientId);
    }
}
