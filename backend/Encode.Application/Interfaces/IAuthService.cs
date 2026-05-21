using Encode.Application.DTO.Auth;

namespace Encode.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequest request);
        Task<string> Login(LoginRequest request);
    }
}
