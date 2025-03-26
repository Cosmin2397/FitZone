namespace FitZone.AuthService.Dtos
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }

}
