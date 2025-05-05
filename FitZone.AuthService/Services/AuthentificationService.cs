using FitZone.AuthService.Dtos;
using FitZone.AuthService.Entities;
using FitZone.AuthService.Repositories;

namespace FitZone.AuthService.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IAuthentificationRepository _authRepository;

        public AuthentificationService(IAuthentificationRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> DeleteUser(string email)
        {
           return await _authRepository.DeleteUser(email);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _authRepository.GetAllUsers();
        }

        public async Task<List<UserDto>> GetGymUsers(Guid gymId)
        {
            return await _authRepository.GetGymUsers(gymId);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            return await _authRepository.GetUserByEmail(email);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            return await _authRepository.GetUserById(id);
        }

        public async Task<LoginResponse> LoginUser(LoginModel user)
        {
            return await _authRepository.LoginUser(user);
        }

        public async Task<bool> LogoutUser(string email)
        {
            return await _authRepository.LogoutUser(email);
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenModel model)
        {
            return await _authRepository.RefreshToken(model);
        }

        public async Task<bool> RegisterUser(RegisterModel user)
        {
            return await _authRepository.RegisterUser(user);
        }

        public async Task<bool> UpdateUser(UpdateUser newUser)
        {
            return await _authRepository.UpdateUser(newUser);
        }

        public async Task<bool> UpdateUserGym(Guid userId, Guid newGym)
        {
            return await _authRepository.UpdateUserGym(userId, newGym);
        }
    }
}
