using FitZone.AuthService.Dtos;
using FitZone.AuthService.Entities;

namespace FitZone.AuthService.Repositories
{
    public interface IAuthentificationRepository
    {
        Task<bool> RegisterUser(RegisterModel user);

        Task<List<UserDto>> GetAllUsers();

        Task<List<UserDto>> GetGymUsers(Guid gymId);

        Task<LoginResponse> LoginUser(LoginModel user);

        Task<LoginResponse> RefreshToken(RefreshTokenModel model);

        Task<bool> LogoutUser(string email);

        Task<bool> DeleteUser(string email);

        Task<bool> UpdateUser(UpdateUser updatetUser);

        Task<bool> UpdateUserGym(Guid userId, Guid newGym);

        Task<UserDto> GetUserByEmail(string email);
    }

}
