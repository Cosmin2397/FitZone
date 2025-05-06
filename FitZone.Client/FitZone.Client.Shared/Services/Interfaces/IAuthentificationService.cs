using FitZone.Client.Shared.DTOs;
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

        Task<bool> Logout(string email);

        Task<LoginResponseDto> RefreshToken(RefreshTokenDto model);

        Task<List<UserDto>> GetUsers();

        Task<UserDto> GetUserByEmail(string email);

        Task<bool> DeleteUser(string email);

        Task<bool> UpdateUser(UserDto user);

        Task<string> GetUserName(Guid clientId);
    }
}
